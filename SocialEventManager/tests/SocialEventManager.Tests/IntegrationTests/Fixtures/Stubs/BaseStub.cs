using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs
{
    public abstract class BaseStub<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        public Task<bool> DeleteAsync(TEntity entity) => Task.FromResult(false);

        public Task<bool> DeleteAsync(Guid id, string columnName) => Task.FromResult(false);

        public Task<bool> DeleteAsync(int id, string columnName) => Task.FromResult(false);

        public Task<TEntity> GetAsync(Guid id) => Task.FromResult(default(TEntity));

        public Task<IEnumerable<TEntity>> GetAsync() => Task.FromResult(default(IEnumerable<TEntity>));

        public Task<IEnumerable<TEntity>> GetAsync<TFilter>(TFilter filterValue, string columnName) => Task.FromResult(default(IEnumerable<TEntity>));

        public Task<IEnumerable<TEntity>> GetAsync<TFilter>(IEnumerable<TFilter> filterValues, string columnName) => Task.FromResult(default(IEnumerable<TEntity>));

        public Task<TEntity> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName) => Task.FromResult(default(TEntity));

        public Task<int> InsertAsync(TEntity entity) => Task.FromResult(0);

        public Task InsertAsync(IEnumerable<TEntity> entities) => Task.CompletedTask;

        public Task<bool> UpdateAsync(TEntity entity) => Task.FromResult(false);
    }
}
