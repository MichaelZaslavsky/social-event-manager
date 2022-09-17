using System.Diagnostics.CodeAnalysis;
using SocialEventManager.BLL.Services.Infrastructure;
using SocialEventManager.BLL.Services.Roles;
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Models.Roles;
using SocialEventManager.Shared.Models.Users;

namespace SocialEventManager.BLL.Services.Users;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public sealed class UserRolesService : ServiceBase<IUserRolesRepository, UserRole>, IUserRolesService
{
    private readonly IRolesService _rolesService;

    public UserRolesService(IUserRolesRepository userRolesRepository, IRolesService rolesService)
        : base(userRolesRepository)
    {
        _rolesService = rolesService;
    }

    public async Task<int> CreateUserRole(UserRoleForCreationDto userRoleForCreation) =>
        await Repository.InsertAsync(userRoleForCreation.UserId, userRoleForCreation.RoleName);

    public async Task<IEnumerable<UserRoleDto>> GetUserRoles(Guid userId)
    {
        IEnumerable<RoleDto> roles = await _rolesService.GetRoles(userId);

        return roles.Any()
            ? roles.Select(role => new UserRoleDto(userId.ToString(), role.Name))
            : throw new NotFoundException($"User '{userId}' roles {ValidationConstants.WasNotFound}");
    }

    public async Task<IEnumerable<UserRoleDto>> GetUserRoles(string roleName)
    {
        IEnumerable<UserRole> userRoles = await Repository.GetUserRoles(roleName);

        return userRoles.Any()
            ? userRoles.Select(ur => new UserRoleDto(ur.UserId, roleName))
            : throw new NotFoundException($"Users of role '{roleName}' {ValidationConstants.WereNotFound}");
    }

    public async Task<bool> DeleteUserRole(UserRoleBase userRoleBase)
    {
        await EnsureUserRoleExists(userRoleBase.UserId, userRoleBase.RoleName);

        return await Repository.DeleteUserRole(userRoleBase.UserId, userRoleBase.RoleName);
    }

    public async Task<bool> IsInRole(UserRoleDto userRole) =>
        await Repository.IsInRole(userRole.UserId, userRole.RoleName);

    #region Private Methods

    private async Task EnsureUserRoleExists(Guid userId, string roleName)
    {
        bool isInRole = await IsInRole(new UserRoleDto(userId, roleName));

        if (!isInRole)
        {
            throw new NotFoundException($"User '{userId}' role '{roleName}' {ValidationConstants.WasNotFound}");
        }
    }

    #endregion Private Methods
}
