using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using SocialEventManager.DLL.Constants;

namespace SocialEventManager.DLL.Infrastructure
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        protected GenericRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            using IDbConnection connection = CreateDbConnection();
            return await connection.InsertAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            using IDbConnection connection = CreateDbConnection();
            await connection.InsertAsync(entities);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            using IDbConnection connection = CreateDbConnection();
            return await connection.GetAsync<TEntity>(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            using IDbConnection connection = CreateDbConnection();
            return await connection.GetAllAsync<TEntity>();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using IDbConnection connection = CreateDbConnection();
            return await connection.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            using IDbConnection connection = CreateDbConnection();
            return await connection.DeleteAsync(entity);
        }

        public async Task<TEntity> GetSingleOfDefaultAsync<TFilter>(TFilter filterValue, string columnName)
        {
            using IDbConnection connection = CreateDbConnection();
            string tableName = GetTableName<TEntity>();

            string query = $@"
                SELECT  *
                FROM    {tableName}
                WHERE   {columnName} = @FilterValue;";

            return await connection.QuerySingleOrDefaultAsync<TEntity>(query, new { FilterValue = filterValue });
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using IDbConnection connection = CreateDbConnection();
            string tableName = GetTableName<TEntity>();

            string query = $@"
                DELETE FROM {tableName}
                WHERE Id = @Id;

                {QueryConstants.SelectRowCount}";

            return await connection.ExecuteAsync(query, new { Id = id }) > 0;
        }

        #region Private Methods

        private IDbConnection CreateDbConnection() =>
            _dbConnectionFactory.CreateDbConnection();

        private static string GetTableName<T>()
        {
            if (SqlMapperExtensions.TableNameMapper != null)
            {
                return SqlMapperExtensions.TableNameMapper(typeof(T));
            }

            const string getTableName = "GetTableName";
            MethodInfo getTableNameMethod = typeof(SqlMapperExtensions).GetMethod(getTableName, BindingFlags.NonPublic | BindingFlags.Static);

            if (getTableNameMethod == null)
            {
                throw new ArgumentOutOfRangeException($"Method '{getTableName}' is not found in '{nameof(SqlMapperExtensions)}' class.");
            }

            return getTableNameMethod.Invoke(null, new object[] { typeof(T) }) as string;
        }

        #endregion Private Methods
    }
}
