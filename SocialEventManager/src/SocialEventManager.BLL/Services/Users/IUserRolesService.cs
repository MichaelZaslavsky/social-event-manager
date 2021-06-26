using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialEventManager.BLL.Models.Users;

namespace SocialEventManager.BLL.Services.Users
{
    public interface IUserRolesService
    {
        Task<int> CreateUserRole(UserRoleForCreationDto userRoleForCreation);

        Task<IEnumerable<UserRoleDto>> GetUserRoles(Guid userId);

        Task<IEnumerable<UserRoleDto>> GetUserRoles(string roleName);

        Task<bool> DeleteUserRole(UserRoleBase userRoleBase);

        Task<bool> IsInRole(UserRoleDto userRole);
    }
}
