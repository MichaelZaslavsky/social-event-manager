using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialEventManager.BLL.Models;

namespace SocialEventManager.BLL.Services
{
    public interface IUserRolesService
    {
        Task<int> CreateUserRole(UserRoleForCreationDto userRoleForCreation);

        Task<IEnumerable<UserRoleDto>> GetUserRoles(Guid userId);

        Task<IEnumerable<UserRoleDto>> GetUserRoles(string roleName);

        Task<bool> DeleteUserRole(BaseUserRoleDto baseUserRole);

        Task<bool> IsInRole(UserRoleDto userRole);
    }
}
