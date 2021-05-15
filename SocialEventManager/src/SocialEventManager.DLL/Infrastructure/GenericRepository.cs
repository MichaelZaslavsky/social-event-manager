using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using SocialEventManager.DLL.Constants;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DLL.Infrastructure
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        protected GenericRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
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
            return await connection.GetListAsync<TEntity>();
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
            dbConnectionFactory.CreateDbConnection();

        #region Private Methods

        private static string GetTableName()
        {
            if (typeof(TEntity).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is not TableAttribute table)
            {
                throw new NullReferenceException(MessagesConstants.InternalServerError);
            }

            return table.Name;
        }

        #endregion Private Methods
    }
}
