using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Models.Accounts;
using SocialEventManager.Shared.Models.Identity;
using SocialEventManager.Shared.Models.Users;
using SocialEventManager.BLL.Services.Accounts;
using SocialEventManager.BLL.Services.Users;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.BLL.Services.Identity;

public class CustomUsersStore :
    IUserPasswordStore<ApplicationUser>,
    IUserEmailStore<ApplicationUser>,
    IUserRoleStore<ApplicationUser>,
    IUserSecurityStampStore<ApplicationUser>,
    IUserClaimStore<ApplicationUser>
{
    private readonly IAccountsService _accountsService;
    private readonly IUserRolesService _userRolesService;
    private readonly IUserClaimsService _userClaimsService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomUsersStore(IAccountsService accountsService, IUserRolesService userRolesService, IUserClaimsService userClaimsService,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _accountsService = accountsService;
        _userRolesService = userRolesService;
        _userClaimsService = userClaimsService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #region Implement IUserStore

    public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        return Task.FromResult(user.Id);
    }

    public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        user.UserName = userName;
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        user.NormalizedUserName = normalizedName ?? throw new ArgumentNullException(nameof(normalizedName));
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        AccountForCreationDto accountForCreation = _mapper.Map<AccountForCreationDto>(user);
        int id = await _accountsService.CreateAccount(accountForCreation);

        return id > 0
            ? IdentityResult.Success
            : IdentityResult.Failed(new IdentityError { Description = UserValidationConstants.CouldNotInsertUser(user.Email) });
    }

    public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        AccountForUpdateDto accountForUpdate = _mapper.Map<AccountForUpdateDto>(user);
        bool isUpdated = await _accountsService.UpdateAccount(accountForUpdate);

        return isUpdated
            ? IdentityResult.Success
            : IdentityResult.Failed(new IdentityError { Description = UserValidationConstants.CouldNotUpdateUser(user.Email) });
    }

    public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        if (!Guid.TryParse(user.Id, out Guid userId))
        {
            throw new FormatException(ValidationConstants.NotAValidIdentifier);
        }

        return DeleteAccountAsync(user, userId);
    }

    public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(userId);

        if (!Guid.TryParse(userId, out Guid id))
        {
            throw new FormatException(ValidationConstants.NotAValidIdentifier);
        }

        return GetAccountAsync(id);
    }

    public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(normalizedUserName);

        AccountDto account = await _accountsService.GetAccountByNormalizedUserName(normalizedUserName);
        return _mapper.Map<ApplicationUser>(account);
    }

    #endregion Implement IUserStore

    #region Implement IUserEmailStore

    public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        user.UserName = email;
        return Task.CompletedTask;
    }

    public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        AccountDto account = await _accountsService.GetAccountByEmail(normalizedEmail);
        return _mapper.Map<ApplicationUser>(account);
    }

    public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        return Task.FromResult(user.NormalizedEmail);
    }

    public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        user.NormalizedEmail = normalizedEmail ?? throw new ArgumentNullException(nameof(normalizedEmail));
        return Task.CompletedTask;
    }

    #endregion Implement IUserEmailStore

    #region Implement IUserPasswordStore

    public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        user.PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        return Task.FromResult(!user.PasswordHash.IsNullOrWhiteSpace());
    }

    #endregion Implement IUserPasswordStore

    #region Implement IUserRoleStore

    public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(roleName, nameof(roleName));

        UserRoleForCreationDto userRoleForCreation = new(user.Id, roleName);

        _unitOfWork.BeginTransaction();
        await _userRolesService.CreateUserRole(userRoleForCreation);
        _unitOfWork.Commit();

        return;
    }

    public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        if (!Guid.TryParse(user.Id, out Guid userId))
        {
            throw new FormatException(ValidationConstants.NotAValidIdentifier);
        }

        return GetUserRoleAsync(userId);
    }

    public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(roleName, nameof(roleName));

        IEnumerable<AccountDto> accounts = await _accountsService.GetAccounts(roleName);
        return _mapper.Map<IList<ApplicationUser>>(accounts);
    }

    public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(roleName, nameof(roleName));

        UserRoleBase userRoleBase = new(user.Id, roleName);

        _unitOfWork.BeginTransaction();
        await _userRolesService.DeleteUserRole(userRoleBase);
        _unitOfWork.Commit();

        return;
    }

    public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);
        ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(roleName, nameof(roleName));

        UserRoleDto userRole = new(user.Id, roleName);
        return await _userRolesService.IsInRole(userRole);
    }

    #endregion Implement IUserRoleStore

    #region Implement IUserSecurityStampStore

    public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        user.SecurityStamp = stamp;

        return Task.CompletedTask;
    }

    public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        return Task.FromResult(user.SecurityStamp);
    }

    #endregion Implement IUserSecurityStampStore

    #region Implement IUserClaimStore

    public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(user);

        if (!Guid.TryParse(user.Id, out Guid userId))
        {
            throw new FormatException(ValidationConstants.NotAValidIdentifier);
        }

        return GetUserClaimsAsync(userId);
    }

    public async Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullExceptionHelpers.ThrowIfNull((user, nameof(user)), (claims, nameof(claims)));

        if (claims.IsEmpty())
        {
            return;
        }

        if (!Guid.TryParse(user.Id, out Guid userId))
        {
            throw new FormatException(ValidationConstants.NotAValidIdentifier);
        }

        await CreateUserClaimsAsync(claims, userId);
    }

    public async Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullExceptionHelpers.ThrowIfNull((user, nameof(user)), (claim, nameof(claim)), (newClaim, nameof(newClaim)));

        if (!Guid.TryParse(user.Id, out Guid userId))
        {
            throw new FormatException(ValidationConstants.NotAValidIdentifier);
        }

        await ReplaceUserClaimAsync(claim, newClaim, userId);
    }

    public async Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullExceptionHelpers.ThrowIfNull((user, nameof(user)), (claims, nameof(claims)));

        if (claims.IsEmpty())
        {
            return;
        }

        if (!Guid.TryParse(user.Id, out Guid userId))
        {
            throw new FormatException(ValidationConstants.NotAValidIdentifier);
        }

        await DeleteUserClaimsAsync(claims, userId);
    }

    public async Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(claim);

        IEnumerable<AccountDto> accounts = await _accountsService.GetAccounts(claim.Value, claim.Value);
        return _mapper.Map<IList<ApplicationUser>>(accounts);
    }

    #endregion Implement IUserClaimStore

    protected virtual void Dispose(bool disposing)
    {
    }

    private async Task<IdentityResult> DeleteAccountAsync(ApplicationUser user, Guid userId)
    {
        bool isDeleted = await _accountsService.DeleteAccount(userId);

        return isDeleted
            ? IdentityResult.Success
            : IdentityResult.Failed(new IdentityError { Description = UserValidationConstants.CouldNotDeleteUser(user.Email) });
    }

    private async Task<ApplicationUser> GetAccountAsync(Guid id)
    {
        AccountDto account = await _accountsService.GetAccount(id);
        return _mapper.Map<ApplicationUser>(account);
    }

    private async Task<IList<string>> GetUserRoleAsync(Guid userId)
    {
        IEnumerable<UserRoleDto> userRoles = await _userRolesService.GetUserRoles(userId);
        return userRoles.Select(ur => ur.RoleName).ToList();
    }

    private async Task<IList<Claim>> GetUserClaimsAsync(Guid userId)
    {
        IEnumerable<UserClaimDto> userClaims = await _userClaimsService.GetUserClaims(userId);
        return userClaims.Select(uc => new Claim(uc.Type, uc.Value)).ToList();
    }

    private async Task CreateUserClaimsAsync(IEnumerable<Claim> claims, Guid userId)
    {
        IEnumerable<UserClaimForCreationDto> userClaimsForCreation =
            _mapper.Map<IEnumerable<UserClaimForCreationDto>>(claims, opt => opt.AfterMap((_, dest) =>
            {
                foreach (UserClaimForCreationDto item in dest)
                {
                    item.UserId = userId;
                }
            }));

        _unitOfWork.BeginTransaction();
        await _userClaimsService.CreateUserClaims(userClaimsForCreation);
        _unitOfWork.Commit();
    }

    private async Task ReplaceUserClaimAsync(Claim claim, Claim newClaim, Guid userId)
    {
        UserClaimDto currentUserClaim = _mapper.Map<UserClaimDto>(claim, opt => opt.AfterMap((_, dest) => dest.UserId = userId));
        UserClaimForUpdateDto newUserClaimForUpdate = _mapper.Map<UserClaimForUpdateDto>(newClaim, opt => opt.AfterMap((_, dest) => dest.UserId = userId));

        await _userClaimsService.ReplaceUserClaim(currentUserClaim, newUserClaimForUpdate);
    }

    private async Task DeleteUserClaimsAsync(IEnumerable<Claim> claims, Guid userId)
    {
        IEnumerable<UserClaimBase> userClaims = _mapper.Map<IEnumerable<UserClaimBase>>(claims);
        await _userClaimsService.DeleteUserClaims(userClaims, userId);
    }
}
