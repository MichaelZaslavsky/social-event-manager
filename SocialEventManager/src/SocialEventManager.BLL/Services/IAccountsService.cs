using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialEventManager.BLL.Models;

namespace SocialEventManager.BLL.Services
{
    public interface IAccountsService
    {
        Task<int> CreateAccount(AccountForCreationDto accountForCreation);

        Task<AccountDto> GetAccount(Guid userId);

        Task<AccountDto> GetAccountByUserName(string userName);

        Task<AccountDto> GetAccountByEmail(string email);

        Task<IEnumerable<AccountDto>> GetAccounts(string roleName);

        Task<bool> UpdateAccount(AccountForUpdateDto accountForUpdate);

        Task<bool> DeleteAccount(Guid userId);
    }
}
