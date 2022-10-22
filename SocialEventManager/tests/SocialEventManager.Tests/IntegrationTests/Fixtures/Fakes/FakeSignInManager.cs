using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SocialEventManager.Shared.Models.Auth;
using SocialEventManager.Tests.Common.DataMembers.Storages.Identity;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Fakes;

public sealed class FakeSignInManager : SignInManager<ApplicationUser>
{
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

    public FakeSignInManager(IPasswordHasher<ApplicationUser> passwordHasher)
        : base(
            new FakeUserManager(passwordHasher),
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<ILogger<SignInManager<ApplicationUser>>>(),
            Mock.Of<IAuthenticationSchemeProvider>(),
            Mock.Of<IUserConfirmation<ApplicationUser>>())
    {
        _passwordHasher = passwordHasher;
    }

    public override Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
    {
        ApplicationUser? applicationUser = UserStorage.Instance.Data.SingleOrDefault(u => u.Email == user.Email);

        if (applicationUser is null)
        {
            return Task.FromResult(SignInResult.Failed);
        }

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value.LocalDateTime > DateTime.UtcNow)
        {
            return Task.FromResult(SignInResult.LockedOut);
        }

        PasswordVerificationResult passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, password);

        SignInResult result;

        if (passwordVerificationResult == PasswordVerificationResult.Success)
        {
            result = SignInResult.Success;
        }
        else
        {
            result = SignInResult.Failed;

            // In real use case it also checks the max failed attempts.
            if (lockoutOnFailure)
            {
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(30);
            }
        }

        return Task.FromResult(result);
    }
}
