using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Exceptions;
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

    public static TheoryData<UserRegistrationDto, HttpStatusCode, string> InvalidUserRegistrationExceptionData =>
        new()
        {
            { GetUserRegistration(firstName: nameof(ValidationException)), HttpStatusCode.BadRequest, ExceptionConstants.BadRequest },
            { GetUserRegistration(firstName: nameof(BadRequestException)), HttpStatusCode.BadRequest, ExceptionConstants.BadRequest },
            { GetUserRegistration(firstName: nameof(NotFoundException)), HttpStatusCode.NotFound, ExceptionConstants.NotFound },
            { GetUserRegistration(firstName: nameof(UnprocessableEntityException)), HttpStatusCode.UnprocessableEntity, ExceptionConstants.UnprocessableEntity },
            { GetUserRegistration(firstName: nameof(NullReferenceException)), HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError },
            { GetUserRegistration(firstName: nameof(ArgumentNullException)), HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError },
            { GetUserRegistration(firstName: nameof(ArgumentException)), HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError },
            { GetUserRegistration(firstName: nameof(TimeoutException)), HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError },
            { GetUserRegistration(firstName: nameof(SqlException)), HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError },
            { GetUserRegistration(firstName: nameof(OutOfMemoryException)), HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError },
            { GetUserRegistration(firstName: nameof(Exception)), HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError },
        };

    public static TheoryData<ConfirmEmailDto> ValidConfirmEmail =>
        new()
        {
            { GetConfirmEmail() },
        };

    public static TheoryData<ConfirmEmailDto, string> NonExistingConfirmEmailUserData =>
        new()
        {
            { GetConfirmEmail(email: TestConstants.OtherValidEmail), AuthConstants.ConfirmEmailFailed },
        };

    public static TheoryData<ConfirmEmailDto, string> InvalidConfirmEmailTokenData =>
        new()
        {
            { GetConfirmEmail(token: TestConstants.SomeText), new IdentityErrorDescriber().InvalidToken().Description },
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
        bool emailConfirmed = true,
        string? passwordHash = null,
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
            EmailConfirmed = emailConfirmed,
            PasswordHash = passwordHash ?? Guid.NewGuid().ToString() + "==",
            LockoutEnd = lockoutEnd,
        };
    }

    public static UserLoginDto GetUserLogin(string email = TestConstants.ValidEmail, string password = TestConstants.ValidPassword) =>
        new(email, password);

    private static UserRegistrationDto GetUserRegistration(
        string firstName = TestConstants.SomeText,
        string lastName = TestConstants.MoreText,
        string email = TestConstants.ValidEmail,
        string password = TestConstants.ValidPassword,
        string confirmPassword = TestConstants.ValidPassword)
    {
        return new(firstName, lastName, email, password, confirmPassword);
    }

    private static ConfirmEmailDto GetConfirmEmail(string email = TestConstants.ValidEmail, string token = TestConstants.ValidToken) =>
        new(email, token.Encode());

    private static ForgotPasswordDto GetForgotPassword(string email = TestConstants.ValidEmail) => new(email);

    private static ResetPasswordDto GetResetPassword(
        string email = TestConstants.ValidEmail,
        string token = TestConstants.ValidToken,
        string newPassword = TestConstants.ValidPassword,
        string confirmPassword = TestConstants.ValidPassword)
    {
        return new(email, token.Encode(), newPassword, confirmPassword);
    }
}
