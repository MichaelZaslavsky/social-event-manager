using SocialEventManager.BLL.Models.Roles;

namespace SocialEventManager.BLL.Services.Roles
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
