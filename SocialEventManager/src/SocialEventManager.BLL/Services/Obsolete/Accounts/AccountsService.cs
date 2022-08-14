// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using AutoMapper;
using SocialEventManager.BLL.Services.Infrastructure;
using SocialEventManager.BLL.Services.Users;
using SocialEventManager.DAL.Repositories.Accounts;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Accounts;
using SocialEventManager.Shared.Models.Users;

namespace SocialEventManager.BLL.Services.Accounts;

public class AccountsService : ServiceBase<IAccountsRepository, Account>, IAccountsService
{
    private readonly IUserClaimsService _userClaimsService;
    private readonly IUserRolesService _userRolesService;

    public AccountsService(IAccountsRepository accountsRepository, IUserClaimsService userClaimsService, IUserRolesService userRolesService, IMapper mapper)
        : base(accountsRepository, mapper)
    {
        _userClaimsService = userClaimsService;
        _userRolesService = userRolesService;
    }

    public async Task<int> CreateAccount(AccountForCreationDto accountForCreation)
    {
        Account account = Mapper.Map<Account>(accountForCreation);
        return await Repository.InsertAsync(account);
    }

    public async Task<AccountDto> GetAccount(Guid userId)
    {
        Account? account = await Repository.GetSingleOrDefaultAsync(userId, nameof(Account.UserId));
        return Mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> GetAccountByNormalizedUserName(string normalizedUserName)
    {
        Account? account = await Repository.GetSingleOrDefaultAsync(normalizedUserName, nameof(Account.NormalizedUserName));
        return Mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> GetAccountByEmail(string normalizedEmail)
    {
        Account? account = await Repository.GetSingleOrDefaultAsync(normalizedEmail, nameof(Account.NormalizedEmail));
        return Mapper.Map<AccountDto>(account);
    }

    public async Task<IEnumerable<AccountDto>> GetAccounts(string roleName)
    {
        IEnumerable<UserRoleDto> userRoles = await _userRolesService.GetUserRoles(roleName);

        if (userRoles.IsEmpty())
        {
            throw new NotFoundException($"Users of role '{roleName}' {ValidationConstants.WereNotFound}");
        }

        IEnumerable<Guid> userIds = userRoles.Select(ur => ur.UserId).Distinct();
        IEnumerable<Account> accounts = await Repository.GetAsync(userIds, nameof(Account.UserId));

        return Mapper.Map<IEnumerable<AccountDto>>(accounts);
    }

    public async Task<IEnumerable<AccountDto>> GetAccounts(string claimType, string claimValue)
    {
        IEnumerable<UserClaimDto> userClaims = await _userClaimsService.GetUserClaims(claimType, claimValue);

        if (userClaims.IsEmpty())
        {
            throw new NotFoundException($"The claim '{claimValue}' of type '{claimType}' {ValidationConstants.WasNotFound}");
        }

        IEnumerable<Guid> userIds = userClaims.Select(uc => uc.UserId).Distinct();
        IEnumerable<Account> accounts = await Repository.GetAsync(userIds, nameof(Account.UserId));

        return Mapper.Map<IEnumerable<AccountDto>>(accounts);
    }

    public async Task<bool> UpdateAccount(AccountForUpdateDto accountForUpdate)
    {
        await EnsureAccountExists(accountForUpdate.UserId);

        Account account = Mapper.Map<Account>(accountForUpdate);
        return await Repository.UpdateAsync(account);
    }

    public async Task<bool> DeleteAccount(Guid userId)
    {
        await EnsureAccountExists(userId);

        return await Repository.DeleteAsync(userId, nameof(Account.UserId));
    }

    #region Private Methods

    private async Task EnsureAccountExists(Guid userId)
    {
        AccountDto account = await GetAccount(userId);

        if (account is null)
        {
            throw new NotFoundException($"The user '{userId}' {ValidationConstants.WasNotFound}");
        }
    }

    #endregion Private Methods
}
*/
