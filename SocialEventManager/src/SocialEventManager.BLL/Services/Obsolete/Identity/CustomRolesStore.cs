using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.BLL.Services.Roles;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Models.Roles;

namespace SocialEventManager.BLL.Services.Identity;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public class CustomRolesStore : IRoleStore<ApplicationRole>
{
    private readonly IRolesService _rolesService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomRolesStore(IRolesService rolesService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _rolesService = rolesService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #region Implement IRoleStore

    public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(role);

        RoleForCreationDto roleForCreation = _mapper.Map<RoleForCreationDto>(role);

        _unitOfWork.BeginTransaction();
        Guid id = await _rolesService.CreateRole(roleForCreation);
        _unitOfWork.Commit();

        return id == Guid.Empty
            ? IdentityResult.Failed(new IdentityError { Description = RoleValidationConstants.CouldNotInsertRole(roleForCreation.Name) })
            : IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(role);

        RoleForUpdateDto roleForUpdate = _mapper.Map<RoleForUpdateDto>(role);
        bool isUpdated = await _rolesService.UpdateRole(roleForUpdate);

        return isUpdated
            ? IdentityResult.Success
            : IdentityResult.Failed(new IdentityError { Description = RoleValidationConstants.CouldNotUpdateRole(role.Name) });
    }

    public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(role);

        return Guid.TryParse(role.Id, out Guid roleId)
            ? DeleteRoleAsync(role, roleId)
            : throw new FormatException(ValidationConstants.NotAValidIdentifier);
    }

    public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(role);

        return Task.FromResult(role.Id);
    }

    public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(role);

        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(role);

        role.Name = roleName ?? throw new ArgumentNullException(nameof(roleName));
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(role);

        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(role);

        role.NormalizedName = normalizedName ?? throw new ArgumentNullException(nameof(normalizedName));
        return Task.CompletedTask;
    }

    public Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(roleId);

        return !Guid.TryParse(roleId, out Guid id) ? throw new FormatException(ValidationConstants.NotAValidIdentifier) : GetRoleAsync(id);
    }

    public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(normalizedRoleName);

        RoleDto role = await _rolesService.GetRole(normalizedRoleName);
        return _mapper.Map<ApplicationRole>(role);
    }

    #endregion Implement IRoleStore

    protected virtual void Dispose(bool disposing)
    {
    }

    private async Task<IdentityResult> DeleteRoleAsync(ApplicationRole role, Guid roleId)
    {
        bool isDeleted = await _rolesService.DeleteRole(roleId);

        return isDeleted
            ? IdentityResult.Success
            : IdentityResult.Failed(new IdentityError { Description = RoleValidationConstants.CouldNotDeleteRole(role.Name) });
    }

    private async Task<ApplicationRole> GetRoleAsync(Guid id)
    {
        RoleDto role = await _rolesService.GetRole(id);
        return _mapper.Map<ApplicationRole>(role);
    }
}
