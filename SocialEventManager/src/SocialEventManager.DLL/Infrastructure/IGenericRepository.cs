using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialEventManager.DLL.Infrastructure
{
    public interface IGenericRepository<TEntity>
    {
        Task<int> InsertAsync(TEntity entity);

        Task InsertAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAsync();

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        Task DeleteAsync(Guid id);
    }
}
