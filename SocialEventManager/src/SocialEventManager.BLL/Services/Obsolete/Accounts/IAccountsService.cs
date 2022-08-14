// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using SocialEventManager.Shared.Models.Accounts;

namespace SocialEventManager.BLL.Services.Accounts;

public interface IAccountsService
{
    Task<int> CreateAccount(AccountForCreationDto accountForCreation);

    Task<AccountDto> GetAccount(Guid userId);

    Task<AccountDto> GetAccountByNormalizedUserName(string normalizedUserName);

    Task<AccountDto> GetAccountByEmail(string normalizedEmail);

    Task<IEnumerable<AccountDto>> GetAccounts(string roleName);

    Task<IEnumerable<AccountDto>> GetAccounts(string claimType, string claimValue);

    Task<bool> UpdateAccount(AccountForUpdateDto accountForUpdate);

    Task<bool> DeleteAccount(Guid userId);
}
*/
