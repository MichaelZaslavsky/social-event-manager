using System;
using System.Threading.Tasks;

namespace SocialEventManager.DLL.Infrastructure
{
    public interface IExtendedRepository<TEntity>
    {
        Task<TEntity> GetSingleOfDefaultAsync<TFilter>(TFilter filterValue, string columnName);

        Task<bool> DeleteAsync(Guid id);
    }
}
