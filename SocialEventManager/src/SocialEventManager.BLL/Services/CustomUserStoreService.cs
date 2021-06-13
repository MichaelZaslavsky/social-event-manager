using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Infrastructure;
using SocialEventManager.DLL.Repositories;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.BLL.Services
{
    public class CustomUserStoreService : IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomUserStoreService(IAccountsRepository accountsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _accountsRepository = accountsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Account account = _mapper.Map<Account>(user);

            _unitOfWork.BeginTransaction();
            int id = await _accountsRepository.InsertAsync(account);
            _unitOfWork.Commit();

            return id > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError { Description = UserValidationConstants.CouldNotInsertUser(user.Email) });
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            Account account = await _accountsRepository.GetSingleOrDefaultAsync(userName, nameof(userName));
            return _mapper.Map<ApplicationUser>(account);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Account account = _mapper.Map<Account>(user);

            _unitOfWork.BeginTransaction();
            bool isUpdated = await _accountsRepository.UpdateAsync(account);
            _unitOfWork.Commit();

            return isUpdated
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError { Description = UserValidationConstants.CouldNotUpdateUser(user.Email) });
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Account account = _mapper.Map<Account>(user);

            _unitOfWork.BeginTransaction();
            bool isDeleted = await _accountsRepository.DeleteAsync(account);
            _unitOfWork.Commit();

            return isDeleted
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError { Description = UserValidationConstants.CouldNotDeleteUser(user.Email) });
        }

        public void Dispose()
        {
        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Account account = await _accountsRepository.GetSingleOrDefaultAsync(normalizedEmail, nameof(Account.Email));
            return _mapper.Map<ApplicationUser>(account);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (!Guid.TryParse(userId, out Guid id))
            {
                throw new ArgumentException(ValidationConstants.NotAValidIdentifier, nameof(userId));
            }

            Account account = await _accountsRepository.GetAsync(id);
            return _mapper.Map<ApplicationUser>(account);
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.UserName);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(!user.PasswordHash.IsNullOrWhiteSpace());
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.UserName = email;
            return Task.FromResult<object>(null);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.NormalizedEmail = normalizedEmail ?? throw new ArgumentNullException(nameof(normalizedEmail));
            return Task.FromResult<object>(null);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.NormalizedUserName = normalizedName ?? throw new ArgumentNullException(nameof(normalizedName));
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.UserName = userName;
            return Task.FromResult<object>(null);
        }
    }
}
