using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Infrastructure;

namespace SocialEventManager.DLL.Repositories
{
    public interface IRolesRepository : IGenericRepository<Role>
    {
        Task<Guid> InsertRole(Role role);

        Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId);
    }
}
