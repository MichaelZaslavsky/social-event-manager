using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Stubs;

internal abstract class StubBase<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    public virtual Task<bool> DeleteAsync(TEntity entity) => Task.FromResult(false);

    public virtual Task<bool> DeleteAsync(Guid id, string columnName) => Task.FromResult(false);

    public virtual Task<bool> DeleteAsync(int id, string columnName) => Task.FromResult(false);

    public virtual Task<TEntity> GetAsync(Guid id) => Task.FromResult<TEntity>(null!);

    public virtual Task<IEnumerable<TEntity>> GetAsync() => Task.FromResult<IEnumerable<TEntity>>(null!);

    public virtual Task<IEnumerable<TEntity>> GetAsync<TFilter>(TFilter filterValue, string columnName) => Task.FromResult<IEnumerable<TEntity>>(null!);

    public virtual Task<IEnumerable<TEntity>> GetAsync<TFilter>(IEnumerable<TFilter> filterValues, string columnName) => Task.FromResult<IEnumerable<TEntity>>(null!);

    public virtual Task<TEntity?> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName) => Task.FromResult<TEntity>(null!)!;

    public virtual Task<int> InsertAsync(TEntity entity) => Task.FromResult(0);

    public virtual Task InsertAsync(IEnumerable<TEntity> entities) => Task.CompletedTask;

    public virtual Task<bool> UpdateAsync(TEntity entity) => Task.FromResult(false);
}
