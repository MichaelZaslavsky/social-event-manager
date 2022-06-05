namespace SocialEventManager.DAL.Infrastructure;

public interface IExtendedRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync<TFilter>(TFilter filterValue, string columnName);

    Task<IEnumerable<TEntity>> GetAsync<TFilter>(IEnumerable<TFilter> filterValues, string columnName);

    Task<TEntity> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName);

    Task<bool> DeleteAsync(Guid id, string columnName);

    Task<bool> DeleteAsync(int id, string columnName);
}
