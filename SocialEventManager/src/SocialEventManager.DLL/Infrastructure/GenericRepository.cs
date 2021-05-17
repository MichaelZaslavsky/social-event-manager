using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task DeleteAsync(Guid id)
        {
            using IDbConnection connection = CreateDbConnection();

            string tableName = GetTableName();
            string query = $@"
                DELETE FROM {tableName}
                WHERE Id = @Id;

                {QueryConstants.SelectRowCount}";

            await connection.ExecuteAsync(query, new { Id = id });
        }

        private IDbConnection CreateDbConnection() =>
            _dbConnectionFactory.CreateDbConnection();

        #region Private Methods

        private static string GetTableName()
        {
            return string.Empty;
            /*
            if (typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is not TableAttribute table)
            {
                throw new NullReferenceException(MessagesConstants.InternalServerError);
            }

            return table.Name;
            */
        }

        #endregion Private Methods
    }
}
