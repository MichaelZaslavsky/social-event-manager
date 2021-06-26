using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SocialEventManager.BLL.Models.Roles;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.BLL.Services.Roles
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateRole(RoleForCreationDto roleForCreation)
        {
            Role role = _mapper.Map<Role>(roleForCreation);
            return await _rolesRepository.InsertRole(role);
        }

        public async Task<RoleDto> GetRole(Guid roleId)
        {
            Role role = await _rolesRepository.GetSingleOrDefaultAsync(roleId, nameof(Role.Id));
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> GetRole(string normalizedRoleName)
        {
            Role role = await _rolesRepository.GetSingleOrDefaultAsync(normalizedRoleName, nameof(Role.NormalizedName));
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IEnumerable<RoleDto>> GetRoles(Guid userId)
        {
            IEnumerable<Role> roles = await _rolesRepository.GetByUserIdAsync(userId);

            if (roles.IsEmpty())
            {
                throw new NotFoundException($"The user roles for user '{userId}' {ValidationConstants.WereNotFound}");
            }

            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<bool> UpdateRole(RoleForUpdateDto roleForUpdate)
        {
            await EnsureRoleExists(roleForUpdate.Id);

            Role role = _mapper.Map<Role>(roleForUpdate);
            return await _rolesRepository.UpdateAsync(role);
        }

        public async Task<bool> DeleteRole(Guid roleId)
        {
            await EnsureRoleExists(roleId);

            return await _rolesRepository.DeleteAsync(roleId, nameof(Role.Id));
        }

        #region Private Methods

        private async Task EnsureRoleExists(Guid roleId)
        {
            Role role = await _rolesRepository.GetAsync(roleId);

            if (role == null)
            {
                throw new NotFoundException($"The role '{roleId}' {ValidationConstants.WasNotFound}");
            }

            return;
        }

        #endregion Private Methods
    }
}
