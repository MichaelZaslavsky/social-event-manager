using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace SocialEventManager.Tests.IntegrationTests.Infrastructure
{
    public static class GenericTableExtensions
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public static async Task CreateTableIfNotExistsAsync<T>(this IDbConnection db, string tableName)
        {
            await ExecWithAliasAsync<T>(tableName, () =>
            {
                db.CreateTableIfNotExists<T>();
                return Task.FromResult<object>(null);
            });
        }

        public static async Task DropTableAsync(this IDbConnection db, Type type, string tableName)
        {
            await ExecWithAliasAsync(type, tableName, () =>
            {
                db.DropTable(type);
                return Task.FromResult<object>(null);
            });
        }

        public static async Task<long> InsertAsync<T>(this IDbConnection db, string tableName, T obj, bool selectIdentity = false) =>
            (long)await ExecWithAliasAsync<T>(tableName, async () => await db.InsertAsync(obj, selectIdentity));

        #region Private Methods

        private static async Task<object> ExecWithAliasAsync<T>(string tableName, Func<Task<object>> fn) =>
            await ExecWithAliasAsync(typeof(T), tableName, fn);

        private static async Task<object> ExecWithAliasAsync(Type type, string tableName, Func<Task<object>> fn)
        {
            ModelDefinition modelDef = type.GetModelMetadata();

            await _semaphore.WaitAsync();
            string hold = modelDef.Alias;

            try
            {
                modelDef.Alias = tableName;
                return await fn();
            }
            finally
            {
                modelDef.Alias = hold;
                _semaphore.Release();
            }
        }

        #endregion Private Methods
    }
}
