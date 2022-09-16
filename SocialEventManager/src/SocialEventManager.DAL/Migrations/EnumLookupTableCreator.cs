using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Serilog;
using SocialEventManager.DAL.Queries;
using SocialEventManager.DAL.Utilities.Enums;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.DAL.Migrations;

public class EnumLookupTableCreator
{
    private const string TypeName = "LoadedValues";
    private readonly SqlConnection _connection;

    public EnumLookupTableCreator(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task Run()
    {
        Log.Debug("[{Database}]: Checking for {SchemaConstants.Enum}...", _connection.Database, SchemaConstants.Enum);

        await EnsureSchemaExists();
        HashSet<string> enumTableNames = await GetEnumTableNames();

        IEnumerable<Type> types = GetEnumTypes(DbTypes.SocialEventManager);
        IEnumerable<Task> upsertLookupTasks = types.Select(type => MergeLookup(type, enumTableNames));

        await Task.WhenAll(upsertLookupTasks);

        Log.Debug("[{Database}]: Synced all platform enum tables.", _connection.Database);
    }

    #region Private Methods

    private async Task EnsureSchemaExists()
    {
        await CreateSchemaIfNotExists();
        await RecreateType();
    }

    private async Task CreateSchemaIfNotExists()
    {
        Log.Debug("[{Database}]: Checking for {Enum}...", _connection.Database, SchemaConstants.Enum);

        int rowCount = await _connection.ExecuteScalarAsync<int>(SchemaQueryHelpers.CreateSchemaIfNotExists(SchemaConstants.Enum));

        if (rowCount == 1)
        {
            Log.Debug("[{Database}]: Created schema {Enum}.", _connection.Database, SchemaConstants.Enum);
        }
    }

    private async Task RecreateType()
    {
        Log.Debug("[{Database}]: Recreating {Enum}.{TypeName}...", _connection.Database, SchemaConstants.Enum, TypeName);
        await _connection.ExecuteNonQueryAsync(TableQueryHelpers.RecreateType(SchemaConstants.Enum, TypeName));
    }

    private async Task<HashSet<string>> GetEnumTableNames()
    {
        string query = TableQueryHelpers.GetTableNames(SchemaConstants.Enum);

        using SqlCommand cmd = new(query, _connection);
        using SqlDataReader reader = await cmd.ExecuteReaderAsync();

        return reader.Cast<IDataRecord>().Select(r => (string)r[0]).ToHashSet();
    }

    private static IEnumerable<Type> GetEnumTypes(DbTypes dbTypes)
    {
        Assembly assembly = Assembly.Load($"{nameof(SocialEventManager)}.{nameof(Infrastructure)}");
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
                DbEntityAttribute dbEntity = (attribute as DbEntityAttribute)!;

                if (dbTypes != dbEntity.DbTypes)
                {
                    continue;
                }

                yield return type;
            }
        }
    }

    private async Task MergeLookup(Type type, HashSet<string> tableNames)
    {
        string tableName = GetTableName(type);

        if (!tableNames.Contains(tableName))
        {
            await CreateTable(tableName);
        }

        MergeValues(type, tableName);
    }

    private static string GetTableName(Type type)
    {
        object? attribute = type.GetCustomAttributes(typeof(EnumTableAttribute), inherit: true).FirstOrDefault();

        return attribute is null
            ? type.Name
            : (attribute as EnumTableAttribute)!.TableName;
    }

    private async Task CreateTable(string tableName)
    {
        Log.Debug("[{Database}]: Creating table {Enum}.{tableName}...", _connection.Database, SchemaConstants.Enum, tableName);

        string command = TableQueryHelpers.CreateBasicTable(SchemaConstants.Enum, tableName);
        await _connection.ExecuteNonQueryAsync(command);

        Log.Debug("[{Database}]: Created table {SchemaConstants.Enum}.{tableName}.", _connection.Database, SchemaConstants.Enum, tableName);
    }

    private void MergeValues(Type type, string tableName)
    {
        using SqlCommand cmd = _connection.CreateCommand();
        cmd.Parameters.AddWithValue("@tableName", tableName);

        SqlCommandBuilder builder = new();
        string escapedTableName = builder.QuoteIdentifier(tableName);

        cmd.CommandText = $@"
            MERGE   {SchemaConstants.Enum}.{escapedTableName} dst
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
                Id = (int)f.GetRawConstantValue()!,
                f.Name,
                f.GetCustomAttribute<DescriptionAttribute>()?.Description,
            })
            .ToDataTable();

        SqlParameter param = cmd.Parameters.AddWithValue("@src", values);
        param.SqlDbType = SqlDbType.Structured;
        param.TypeName = $"{SchemaConstants.Enum}.{TypeName}";

        cmd.ExecuteNonQuery();

        Log.Debug("Synced enums values for {tableName}.", tableName);
    }

    #endregion Private Methods
}
