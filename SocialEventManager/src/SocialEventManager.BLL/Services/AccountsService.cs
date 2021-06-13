using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialEventManager.BLL.Models;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Repositories;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.BLL.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUserRolesService _userRolesService;
        private readonly IMapper _mapper;

        public AccountsService(IAccountsRepository accountsRepository, IUserRolesService userRolesService, IMapper mapper)
        {
            _accountsRepository = accountsRepository;
            _userRolesService = userRolesService;
            _mapper = mapper;
        }

        public async Task<int> CreateAccount(AccountForCreationDto accountForCreation)
        {
            Account account = _mapper.Map<Account>(accountForCreation);
            return await _accountsRepository.InsertAsync(account);
        }

        public async Task<AccountDto> GetAccount(Guid userId)
        {
            Account account = await _accountsRepository.GetSingleOrDefaultAsync(userId, nameof(Account.UserId));
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> GetAccountByUserName(string userName)
        {
            Account account = await _accountsRepository.GetSingleOrDefaultAsync(userName, nameof(Account.UserName));
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> GetAccountByEmail(string email)
        {
            Account account = await _accountsRepository.GetSingleOrDefaultAsync(email, nameof(Account.Email));
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<IEnumerable<AccountDto>> GetAccounts(string roleName)
        {
            IEnumerable<UserRoleDto> userRoles = await _userRolesService.GetUserRoles(roleName);

            if (userRoles.IsEmpty())
            {
                throw new NotFoundException($"Users of role '{roleName}' {ValidationConstants.WereNotFound}");
            }

            IEnumerable<Guid> userIds = userRoles.Select(ur => ur.UserId).Distinct();
            IEnumerable<Account> accounts = await _accountsRepository.GetAsync(userIds, nameof(Account.UserId));

            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }

        public async Task<bool> UpdateAccount(AccountForUpdateDto accountForUpdate)
        {
            Account account = _mapper.Map<Account>(accountForUpdate);
            return await _accountsRepository.UpdateAsync(account);
        }

        public async Task<bool> DeleteAccount(Guid userId) =>
            await _accountsRepository.DeleteAsync(userId, nameof(Account.UserId));
    }
}