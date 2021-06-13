using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialEventManager.DLL.Infrastructure
{
    public interface IExtendedRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync<TFilter>(IEnumerable<TFilter> filterValues, string columnName);

        Task<TEntity> GetSingleOrDefaultAsync<TFilter>(TFilter filterValue, string columnName);

        Task<bool> DeleteAsync(Guid id, string columnName);
    }
}
