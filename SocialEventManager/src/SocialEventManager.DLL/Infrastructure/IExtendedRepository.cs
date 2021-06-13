using System;
using System.Threading.Tasks;

namespace SocialEventManager.DLL.Infrastructure
{
    public interface IExtendedRepository<TEntity>
    {
        Task<TEntity> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName);

        Task<bool> DeleteAsync(Guid id);
    }
}
