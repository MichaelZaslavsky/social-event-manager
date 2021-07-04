using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialEventManager.BLL.Models.Roles;
using SocialEventManager.BLL.Models.Users;
using SocialEventManager.BLL.Services.Roles;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Exceptions;

namespace SocialEventManager.BLL.Services.Users
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IRolesService _rolesService;

        public UserRolesService(IUserRolesRepository userRolesRepository, IRolesService rolesService)
        {
            _userRolesRepository = userRolesRepository;
            _rolesService = rolesService;
        }

        public async Task<int> CreateUserRole(UserRoleForCreationDto userRoleForCreation) =>
            await _userRolesRepository.InsertAsync(userRoleForCreation.UserId, userRoleForCreation.RoleName);

        public async Task<IEnumerable<UserRoleDto>> GetUserRoles(Guid userId)
        {
            IEnumerable<RoleDto> roles = await _rolesService.GetRoles(userId);

            return roles.Any()
                ? roles.Select(role => new UserRoleDto(userId.ToString(), role.Name))
                : throw new NotFoundException($"User '{userId}' roles {ValidationConstants.WasNotFound}");
        }

        public async Task<IEnumerable<UserRoleDto>> GetUserRoles(string roleName)
        {
            IEnumerable<UserRole> userRoles = await _userRolesRepository.GetUserRoles(roleName);

            return userRoles.Any()
                ? userRoles.Select(ur => new UserRoleDto(ur.UserId, roleName))
                : throw new NotFoundException($"Users of role '{roleName}' {ValidationConstants.WereNotFound}");
        }

        public async Task<bool> DeleteUserRole(UserRoleBase userRoleBase)
        {
            await EnsureUserRoleExists(userRoleBase.UserId, userRoleBase.RoleName);

            return await _userRolesRepository.DeleteUserRole(userRoleBase.UserId, userRoleBase.RoleName);
        }

        public async Task<bool> IsInRole(UserRoleDto userRole) =>
            await _userRolesRepository.IsInRole(userRole.UserId, userRole.RoleName);

        #region Private Methods

        private async Task EnsureUserRoleExists(Guid userId, string roleName)
        {
            bool isInRole = await IsInRole(new UserRoleDto(userId, roleName));

            if (!isInRole)
            {
                throw new NotFoundException($"User '{userId}' role '{roleName}' {ValidationConstants.WasNotFound}");
            }

            return;
        }

        #endregion Private Methods
    }
}