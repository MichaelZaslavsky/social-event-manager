namespace SocialEventManager.DAL.Infrastructure
{
    public interface IGenericRepository<TEntity> : IExtendedRepository<TEntity>
        where TEntity : class
    {
        Task<int> InsertAsync(TEntity entity);

        Task InsertAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAsync();

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);
    }
}
