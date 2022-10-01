using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Accounts;

namespace SocialEventManager.BLL.Services.Accounts;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
public interface IAccountsService
{
    Task<int> CreateAccount(AccountForCreationDto accountForCreation);

    Task<AccountDto?> GetAccount(Guid userId);

    Task<AccountDto?> GetAccountByNormalizedUserName(string normalizedUserName);

    Task<AccountDto?> GetAccountByEmail(string normalizedEmail);

    Task<IEnumerable<AccountDto>> GetAccounts(string roleName);

    Task<IEnumerable<AccountDto>> GetAccounts(string claimType, string claimValue);

    Task<bool> UpdateAccount(AccountForUpdateDto accountForUpdate);

    Task<bool> DeleteAccount(Guid userId);
}
