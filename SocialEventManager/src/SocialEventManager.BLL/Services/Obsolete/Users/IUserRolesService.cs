using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Users;

namespace SocialEventManager.BLL.Services.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
public interface IUserRolesService
{
    Task<int> CreateUserRole(UserRoleForCreationDto userRoleForCreation);

    Task<IEnumerable<UserRoleDto>> GetUserRoles(Guid userId);

    Task<IEnumerable<UserRoleDto>> GetUserRoles(string roleName);

    Task<bool> DeleteUserRole(UserRoleBase userRoleBase);

    Task<bool> IsInRole(UserRoleDto userRole);
}
