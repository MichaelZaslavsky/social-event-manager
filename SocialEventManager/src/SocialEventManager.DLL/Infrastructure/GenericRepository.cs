using System;
using System.Collections.Generic;
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
        private readonly IDbSession _session;

        protected GenericRepository(IDbSession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task<int> InsertAsync(TEntity entity) =>
            await _session.Connection.InsertAsync(entity, _session.Transaction);

        public async Task InsertAsync(IEnumerable<TEntity> entities) =>
            await _session.Connection.InsertAsync(entities, _session.Transaction);

        public async Task<TEntity> GetAsync(Guid id) =>
            await _session.Connection.GetAsync<TEntity>(id, _session.Transaction);

        public async Task<IEnumerable<TEntity>> GetAsync() =>
            await _session.Connection.GetAllAsync<TEntity>(_session.Transaction);

        public async Task<bool> UpdateAsync(TEntity entity) =>
            await _session.Connection.UpdateAsync(entity, _session.Transaction);

        public async Task<bool> DeleteAsync(TEntity entity) =>
            await _session.Connection.DeleteAsync(entity, _session.Transaction);

        public async Task<TEntity> GetSingleOfDefaultAsync<TFilter>(TFilter filterValue, string columnName)
        {
            string tableName = GetTableName<TEntity>();

            string query = $@"
                SELECT  *
                FROM    {tableName}
                WHERE   {columnName} = @FilterValue;";

            return await _session.Connection.QuerySingleOrDefaultAsync<TEntity>(query, new { FilterValue = filterValue }, _session.Transaction);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            string tableName = GetTableName<TEntity>();

            string query = $@"
                DELETE FROM {tableName}
                WHERE Id = @Id;

                {QueryConstants.SelectRowCount}";

            return await _session.Connection.ExecuteAsync(query, new { Id = id }, _session.Transaction) > 0;
        }

        #region Private Methods

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
