using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Serilog;
using SocialEventManager.DLL.Utilities.Enums;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers.Queries;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.Infrastructure.Migrations
{
    public class EnumLookupTableCreator
    {
        private const string TypeName = "LoadedValues";
        private static ILogger _logger;
        private static SqlConnection _connection;

        public EnumLookupTableCreator(ILogger logger, SqlConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public async Task Run()
        {
            _logger.Debug($"[{_connection.Database}]: Checking for {SchemaConstants.Enum}...");

            await EnsureSchemaExists();
            HashSet<string> enumTableNames = await GetEnumTableNames();

            IEnumerable<Type> types = GetEnumTypes(DbTypes.SocialEventManager);
            IEnumerable<Task> upsertLookupTasks = types.Select(type => MergeLookup(type, enumTableNames));

            await Task.WhenAll(upsertLookupTasks);

            _logger.Debug($"[{_connection.Database}]: Synced all platform enum tables.");
        }

        #region Private Methods

        private static async Task EnsureSchemaExists()
        {
            await CreateSchemaIfNotExists();
            await RecreateType();
        }

        private static async Task CreateSchemaIfNotExists()
        {
            _logger.Debug($"[{_connection.Database}]: Checking for {SchemaConstants.Enum}...");

            int rowCount = await _connection.ExecuteScalarAsync<int>(SchemaQueryHelpers.CreateSchemaIfNotExists(SchemaConstants.Enum));

            if (rowCount == 1)
            {
                _logger.Debug($"[{_connection.Database}]: Created schema {SchemaConstants.Enum}.");
            }
        }

        private static async Task RecreateType()
        {
            _logger.Debug($"[{_connection.Database}]: Recreating {SchemaConstants.Enum}.{TypeName}...");
            await _connection.ExecuteNonQueryAsync(TableQueryHelpers.RecreateType(SchemaConstants.Enum, TypeName));
        }

        private static async Task<HashSet<string>> GetEnumTableNames()
        {
            string query = TableQueryHelpers.GetTableNames(SchemaConstants.Enum);

            using var cmd = new SqlCommand(query, _connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            return reader.Cast<IDataRecord>().Select(r => (string)r[0]).ToHashSet();
        }

        private static IEnumerable<Type> GetEnumTypes(DbTypes dbTypes)
        {
            Assembly assembly = Assembly.Load($"{nameof(SocialEventManager)}.{nameof(DLL)}");
            return GetEnumTypes(dbTypes, assembly);
        }

        private static IEnumerable<Type> GetEnumTypes(DbTypes dbTypes, Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes().Where(t => t.IsEnum))
            {
                object[] attributes = type.GetCustomAttributes(typeof(DbEntityAttribute), inherit: true);

                if (attributes.Length == 0)
                {
                    continue;
                }

                foreach (object attribute in attributes)
                {
                    DbEntityAttribute dbEntity = attribute as DbEntityAttribute;

                    if (dbTypes != dbEntity.DbTypes)
                    {
                        continue;
                    }

                    yield return type;
                }
            }
        }

        private static async Task MergeLookup(Type type, HashSet<string> tableNames)
        {
            string tableName = type.Name;

            if (!tableNames.Contains(tableName))
            {
                await CreateTable(tableName);
            }

            MergeValues(type, tableName);
        }

        private static async Task CreateTable(string tableName)
        {
            _logger.Debug($"[{_connection.Database}]: Creating table {SchemaConstants.Enum}.{tableName}...");

            string command = TableQueryHelpers.CreateBasicTable(SchemaConstants.Enum, tableName);
            await _connection.ExecuteNonQueryAsync(command);

            _logger.Debug($"[{_connection.Database}]: Created table {SchemaConstants.Enum}.{tableName}.");
        }

        private static void MergeValues(Type type, string tableName)
        {
            using SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = $@"
                    MERGE   {SchemaConstants.Enum}.{tableName} dst
                    USING   @src as src ON dst.Id = Src.Id
                    WHEN MATCHED THEN
                        UPDATE SET dst.[Name] = src.[Name], dst.[Description] = src.[Description]
                    WHEN NOT MATCHED BY TARGET THEN
                        INSERT (Id, [Name], [Description])
                        VALUES (src.Id, src.[Name], src.[Description])
                    WHEN NOT MATCHED BY SOURCE THEN
                        DELETE;";

            DataTable values = type.GetFields()
                .Where(f =>
                    f.DeclaringType == type // Don't get stuff inherited from Enum base class/type.
                    && (f.Attributes & FieldAttributes.Literal) != 0) // Get constants, ignore compiler-generated stuff.
                .Select(f => new
                {
                    Id = (int)f.GetRawConstantValue(),
                    f.Name,
                    f.GetCustomAttribute<DescriptionAttribute>()?.Description,
                })
                .ToDataTable();

            SqlParameter param = cmd.Parameters.AddWithValue("@src", values);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = $"{SchemaConstants.Enum}.{TypeName}";

            cmd.ExecuteNonQuery();

            _logger.Debug($"Synced enums values for {tableName}.");
        }

        #endregion Private Methods
    }
}
