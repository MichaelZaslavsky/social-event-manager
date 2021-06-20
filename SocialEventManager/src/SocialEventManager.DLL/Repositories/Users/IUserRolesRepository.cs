using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Infrastructure;

namespace SocialEventManager.DLL.Repositories.Users
{
    public interface IUserRolesRepository : IGenericRepository<UserRole>
    {
        Task<int> InsertAsync(Guid userId, string roleName);

        Task<IEnumerable<UserRole>> GetUserRoles(string roleName);

        Task<bool> DeleteUserRole(Guid userId, string roleName);

        Task<bool> IsInRole(Guid userId, string roleName);
    }
}
