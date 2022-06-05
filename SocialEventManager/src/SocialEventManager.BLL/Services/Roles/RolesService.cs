using AutoMapper;
using SocialEventManager.BLL.Models.Roles;
using SocialEventManager.BLL.Services.Infrastructure;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.BLL.Services.Roles;

public class RolesService : ServiceBase<IRolesRepository, Role>, IRolesService
{
    public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        : base(rolesRepository, mapper)
    {
    }

    public async Task<Guid> CreateRole(RoleForCreationDto roleForCreation)
    {
        Role role = Mapper.Map<Role>(roleForCreation);
        return await Repository.InsertRole(role);
    }

    public async Task<RoleDto> GetRole(Guid roleId)
    {
        Role role = await Repository.GetSingleOrDefaultAsync(roleId, nameof(Role.Id));
        return Mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> GetRole(string normalizedRoleName)
    {
        Role role = await Repository.GetSingleOrDefaultAsync(normalizedRoleName, nameof(Role.NormalizedName));
        return Mapper.Map<RoleDto>(role);
    }

    public async Task<IEnumerable<RoleDto>> GetRoles(Guid userId)
    {
        IEnumerable<Role> roles = await Repository.GetByUserIdAsync(userId);

        if (roles.IsEmpty())
        {
            throw new NotFoundException($"The user roles for user '{userId}' {ValidationConstants.WereNotFound}");
        }

        return Mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    public async Task<bool> UpdateRole(RoleForUpdateDto roleForUpdate)
    {
        await EnsureRoleExists(roleForUpdate.Id);

        Role role = Mapper.Map<Role>(roleForUpdate);
        return await Repository.UpdateAsync(role);
    }

    public async Task<bool> DeleteRole(Guid roleId)
    {
        await EnsureRoleExists(roleId);

        return await Repository.DeleteAsync(roleId, nameof(Role.Id));
    }

    #region Private Methods

    private async Task EnsureRoleExists(Guid roleId)
    {
        Role role = await Repository.GetAsync(roleId);

        if (role is null)
        {
            throw new NotFoundException($"The role '{roleId}' {ValidationConstants.WasNotFound}");
        }

        return;
    }

    #endregion Private Methods
}
