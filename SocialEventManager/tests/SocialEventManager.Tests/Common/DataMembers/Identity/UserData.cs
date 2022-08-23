using System.Globalization;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Auth;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers.Identity;

internal static class UserData
{
    public static readonly Guid Id = Guid.NewGuid();

    public static TheoryData<UserRegistrationDto> ValidUserRegistration =>
        new()
        {
            { GetUserRegistration(email: TestConstants.OtherValidEmail) },
        };

    public static TheoryData<UserRegistrationDto, string> ExistingUserRegistrationData =>
        new()
        {
            {
                GetUserRegistration(email: TestConstants.ValidEmail),
                new IdentityErrorDescriber().DuplicateEmail(TestConstants.ValidEmail).Description
            },
        };

    public static TheoryData<UserRegistrationDto, string> InvalidUserRegistrationData =>
        new()
        {
            {
                GetUserRegistration(email: TestConstants.SomeText),
                ValidationConstants.InvalidEmail(nameof(UserRegistrationDto.Email))
            },
            {
                GetUserRegistration(email: null!),
                ValidationConstants.TheFieldIsRequired(nameof(UserRegistrationDto.Email))
            },
        };

    public static TheoryData<UserLoginDto> ValidUserLogin =>
        new()
        {
            {
                GetUserLogin(TestConstants.ValidEmail)
            },
        };

    public static TheoryData<UserLoginDto, string> NonExistingUserLoginData =>
        new()
        {
            {
                GetUserLogin(TestConstants.OtherValidEmail),
                AuthConstants.EmailOrPasswordIsIncorrect
            },
        };

    public static TheoryData<ForgotPasswordDto> ExistingForgotPasswordUser =>
        new()
        {
            { GetForgotPassword(TestConstants.ValidEmail) },
        };

    public static TheoryData<ForgotPasswordDto> NonExistingForgotPasswordUser =>
        new()
        {
            { GetForgotPassword(TestConstants.OtherValidEmail) },
        };

    public static TheoryData<ResetPasswordDto> ExistingResetPasswordUser =>
        new()
        {
            { GetResetPassword(TestConstants.ValidEmail, newPassword: TestConstants.ValidPassword + "a", confirmPassword: TestConstants.ValidPassword + "a") },
        };

    public static TheoryData<ResetPasswordDto, string> NonExistingResetPasswordUserData =>
        new()
        {
            {
                GetResetPassword(TestConstants.OtherValidEmail),
                AuthConstants.ResetPasswordFailed
            },
        };

    public static TheoryData<ResetPasswordDto, string> InvalidTokenResetPasswordData =>
        new()
        {
            {
                GetResetPassword(TestConstants.ValidEmail, token: TestConstants.SomeText),
                new IdentityErrorDescriber().InvalidToken().Description
            },
        };

    public static ApplicationUser GetUser(
        Guid? userId = null,
        string firstName = TestConstants.SomeText,
        string lastName = TestConstants.MoreText,
        string email = TestConstants.ValidEmail,
        DateTimeOffset? lockoutEnd = null)
    {
        string normalizedEmail = email.ToUpper(CultureInfo.InvariantCulture);

        return new()
        {
            Id = userId ?? Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            NormalizedEmail = normalizedEmail,
            UserName = email,
            NormalizedUserName = normalizedEmail,
            LockoutEnd = lockoutEnd,
        };
    }

    public static UserLoginDto GetUserLogin(string email = TestConstants.ValidEmail, string password = TestConstants.ValidPassword)
    {
        return new()
        {
            Email = email,
            Password = password,
        };
    }

    private static UserRegistrationDto GetUserRegistration(
        string firstName = TestConstants.SomeText,
        string lastName = TestConstants.MoreText,
        string email = TestConstants.ValidEmail,
        string password = TestConstants.ValidPassword,
        string confirmPassword = TestConstants.ValidPassword)
    {
        return new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            ConfirmPassword = confirmPassword,
        };
    }

    private static ForgotPasswordDto GetForgotPassword(string email = TestConstants.ValidEmail) => new() { Email = email };

    private static ResetPasswordDto GetResetPassword(
        string email = TestConstants.ValidEmail,
        string token = TestConstants.ValidToken,
        string newPassword = TestConstants.ValidPassword,
        string confirmPassword = TestConstants.ValidPassword)
    {
        return new()
        {
            Email = email,
            Token = token.Encode(),
            NewPassword = newPassword,
            ConfirmPassword = confirmPassword,
        };
    }
}
