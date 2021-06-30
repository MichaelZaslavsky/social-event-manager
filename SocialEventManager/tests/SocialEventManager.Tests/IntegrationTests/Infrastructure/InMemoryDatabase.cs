using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using ServiceStack.OrmLite;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.Tests.IntegrationTests.Infrastructure
{
    public class InMemoryDatabase : IInMemoryDatabase
    {
        private readonly OrmLiteConnectionFactory _dbFactory;

        public InMemoryDatabase(string connectionString)
        {
            _dbFactory = new(connectionString, SqlServerDialect.Provider);
        }

        public async Task<IDbConnection> OpenConnectionAsync() =>
            await _dbFactory.OpenDbConnectionAsync(CancellationToken.None);

        public IDbTransaction OpenTransaction() => _dbFactory.AlwaysReturnTransaction;

        public async Task InsertAsync<T>(T item)
        {
            using IDbConnection db = await OpenConnectionAsync();

            string tableName = SqlMapperUtilities.GetTableName<T>(includeSchema: false);
            await db.InsertAsync(tableName, item);
        }

        public async Task InsertAsync<T>(IEnumerable<T> items)
        {
            using IDbConnection db = await OpenConnectionAsync();

            string tableName = SqlMapperUtilities.GetTableName<T>(includeSchema: false);

            foreach (T item in items)
            {
                await db.InsertAsync(tableName, item);
            }
        }

        public async Task CreateTableIfNotExistsAsync<T>()
        {
            using IDbConnection db = await OpenConnectionAsync();

            string tableName = SqlMapperUtilities.GetTableName<T>(includeSchema: false);
            await db.CreateTableIfNotExistsAsync<T>(tableName);
        }

        public async Task CleanupAsync()
        {
            using IDbConnection db = await OpenConnectionAsync();

            Assembly assembly = Assembly.Load($"{nameof(SocialEventManager)}.{nameof(DAL)}");
            IEnumerable<Type> types = GetTypes(assembly);
            await DropTablesAsync(db, types);
        }

        #region Private Methods

        private static IEnumerable<Type> GetTypes(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(t => string.Equals(t.Namespace, $"{nameof(SocialEventManager)}.{nameof(DAL)}.{nameof(DAL.Entities)}", StringComparison.Ordinal)
                    && t.IsClass
                    && (t.GetCustomAttributes(typeof(TableAttribute), inherit: true).Length != 0));
        }

        private static async Task DropTablesAsync(IDbConnection db, IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                string tableName = SqlMapperUtilities.GetTableName(type, includeSchema: false);
                await db.DropTableAsync(type, tableName);
            }
        }

        #endregion Private Methods
    }
}
