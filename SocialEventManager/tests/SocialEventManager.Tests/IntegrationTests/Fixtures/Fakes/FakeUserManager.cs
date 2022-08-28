using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SocialEventManager.Shared.Models.Auth;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers.Storages.Identity;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Fakes;

public class FakeUserManager : UserManager<ApplicationUser>
{
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

    public FakeUserManager(IPasswordHasher<ApplicationUser> passwordHasher)
        : base(
            Mock.Of<IUserStore<ApplicationUser>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            passwordHasher,
            Array.Empty<IUserValidator<ApplicationUser>>(),
            Array.Empty<IPasswordValidator<ApplicationUser>>(),
            Mock.Of<ILookupNormalizer>(),
            Mock.Of<IdentityErrorDescriber>(),
            Mock.Of<IServiceProvider>(),
            Mock.Of<ILogger<UserManager<ApplicationUser>>>())
    {
        _passwordHasher = passwordHasher;
    }

    public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        if (UserStorage.Instance.Data.Select(u => u.Email).Contains(user.Email))
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityErrorDescriber().DuplicateEmail(user.Email)));
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, password);
        user.Id = Guid.NewGuid();
        UserStorage.Instance.Data.Add(user);

        return Task.FromResult(IdentityResult.Success);
    }

    public override Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
    {
        if (!UserStorage.Instance.Data.Select(u => u.Email).Contains(user.Email))
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityErrorDescriber().InvalidEmail(user.Email)));
        }

        Guid? roleId = RoleStorage.Instance.Data.SingleOrDefault(r => r.Name == role)?.Id;

        if (roleId is null)
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityErrorDescriber().InvalidRoleName(role)));
        }

        UserRoleStorage.Instance.Data.Add(new()
        {
            UserId = user.Id,
            RoleId = roleId.Value,
        });

        return Task.FromResult(IdentityResult.Success);
    }

    public override Task<ApplicationUser> FindByEmailAsync(string email) =>
        Task.FromResult(UserStorage.Instance.Data.SingleOrDefault(u => u.Email == email))!;

    public override Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user) => Task.FromResult(TestConstants.ValidToken);

    public override Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
    {
        if (token != TestConstants.ValidToken)
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityErrorDescriber().InvalidToken()));
        }

        ApplicationUser? applicationUser = UserStorage.Instance.Data.SingleOrDefault(u => u.Email == user.Email);

        if (applicationUser is null)
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityErrorDescriber().InvalidEmail(user.Email)));
        }

        applicationUser.EmailConfirmed = true;

        return Task.FromResult(IdentityResult.Success);
    }

    public override Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user) => Task.FromResult(TestConstants.ValidToken);

    public override Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
    {
        if (token != TestConstants.ValidToken)
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityErrorDescriber().InvalidToken()));
        }

        ApplicationUser? applicationUser = UserStorage.Instance.Data.SingleOrDefault(u => u.Email == user.Email);

        if (applicationUser is null)
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityErrorDescriber().InvalidEmail(user.Email)));
        }

        applicationUser.PasswordHash = _passwordHasher.HashPassword(user, newPassword);

        return Task.FromResult(IdentityResult.Success);
    }
}
