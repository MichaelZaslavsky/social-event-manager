using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialEventManager.BLL.Models;

namespace SocialEventManager.BLL.Services
{
    public interface IRolesService
    {
        Task<Guid> CreateRole(RoleForCreationDto roleForCreation);

        Task<RoleDto> GetRole(Guid roleId);

        Task<RoleDto> GetRole(string normalizedRoleName);

        Task<IEnumerable<RoleDto>> GetRoles(Guid userId);

        Task<bool> UpdateRole(RoleForUpdateDto roleForUpdate);

        Task<bool> DeleteRole(Guid roleId);
    }
}
