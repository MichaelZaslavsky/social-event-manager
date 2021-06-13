using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SocialEventManager.BLL.Models;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Repositories;

namespace SocialEventManager.BLL.Services
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
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<bool> UpdateRole(RoleForUpdateDto roleForUpdate)
        {
            Role role = _mapper.Map<Role>(roleForUpdate);
            return await _rolesRepository.UpdateAsync(role);
        }

        public async Task<bool> DeleteRole(Guid roleId) =>
            await _rolesRepository.DeleteAsync(roleId, nameof(Role.Id));
    }
}
