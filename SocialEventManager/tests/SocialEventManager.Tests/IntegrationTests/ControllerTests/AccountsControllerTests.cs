using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using netDumbster.smtp;
using SocialEventManager.Infrastructure.Auth;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Auth;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.DataMembers.Identity;
using SocialEventManager.Tests.Common.DataMembers.Storages.Identity;
using SocialEventManager.Tests.Common.Helpers;
using SocialEventManager.Tests.IntegrationTests.Fixtures;
using SocialEventManager.Tests.IntegrationTests.Fixtures.Fakes;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests;

[Collection(TestConstants.StorageDependent)]
[IntegrationTest]
[Category(CategoryConstants.Identity)]
public sealed class AccountsControllerTests : IntegrationTest
{
    public AccountsControllerTests(ApiWebApplicationFactory fixture, IJwtHandler jwtHandler)
      : base(fixture, jwtHandler)
    {
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserRegistration), MemberType = typeof(UserData))]
    public async Task Register_Should_ReturnOk_When_UserIsValid(UserRegistrationDto userRegistration)
    {
        SimpleSmtpServer smtp = SimpleSmtpServer.Start(EmailData.FakePort);
        int initialCount = UserStorage.Instance.Data.Count;

        await Client.CreateAsync(TestApiPathConstants.AccountsRegister, userRegistration);
        await BackgroundJobHelpers.WaitForCompletion(BackgroundJobType.Email);

        UserStorage.Instance.Data.Should().HaveCount(initialCount + 1);

        ApplicationUser actual = UserStorage.Instance.Data.Last();
        AssertApplicationUser(userRegistration, actual);

        UserRoleStorage.Instance.Data.Should().ContainSingle(ur => ur.UserId == actual.Id);
        IdentityUserRole<Guid> actualUserRole = UserRoleStorage.Instance.Data.Single(ur => ur.UserId == actual.Id);
        IdentityRole<Guid> actualRole = RoleStorage.Instance.Data.Single(r => r.Id == actualUserRole.RoleId);
        AssertRole(UserRoles.User, actualRole);
        AssertSmtpEmail(userRegistration, smtp);

        smtp.Stop();
    }

    [Theory]
    [MemberData(nameof(UserData.InvalidUserRegistrationData), MemberType = typeof(UserData))]
    public async Task Register_Should_ReturnBadRequest_When_UserIsInvalid(UserRegistrationDto userRegistration, string expected)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsRegister, userRegistration);
        statusCode.Should().Be(HttpStatusCode.BadRequest);
        message.Should().Contain(expected);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);
    }

    [Theory]
    [MemberData(nameof(UserData.InvalidUserRegistrationExceptionData), MemberType = typeof(UserData))]
    public async Task Register_Should_NotReturnOkStatus_When_CreateUserReturnsException(
        UserRegistrationDto userRegistration, HttpStatusCode expectedStatusCode, string expectedMessage)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        HttpClient client = Factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services => services.AddTransient<UserManager<ApplicationUser>, FakeInvalidUserManager>()))
                .CreateClient(new());

        (HttpStatusCode statusCode, string message) = await client.CreateAsyncWithError(TestApiPathConstants.AccountsRegister, userRegistration);
        statusCode.Should().Be(expectedStatusCode);
        message.Should().Contain(expectedMessage);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);
    }

    [Theory]
    [MemberData(nameof(UserData.ExistingUserRegistrationData), MemberType = typeof(UserData))]
    public async Task Register_Should_ReturnUnprocessableEntity_When_UserAlreadyExists(UserRegistrationDto userRegistration, string expected)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsRegister, userRegistration);
        statusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        message.Should().Contain(expected);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);
    }

    [Theory]
    [MemberData(nameof(UserData.ValidConfirmEmail), MemberType = typeof(UserData))]
    public async Task ConfirmEmail_ReturnOk_When_DataIsValid(ConfirmEmailDto confirmEmail)
    {
        FormUrlEncodedContent formContent = ConvertToFormEncodedContent(confirmEmail);

        HttpRequestMessage request = new(HttpMethod.Post, TestApiPathConstants.AccountsConfirmEmail) { Content = formContent };
        HttpResponseMessage actual = await Client.SendAsync(request);
        actual.StatusCode.Should().Be(HttpStatusCode.OK);

        bool actualEmailConfirmed = UserStorage.Instance.Data.Single(u => u.Email == confirmEmail.Email).EmailConfirmed;
        actualEmailConfirmed.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(UserData.NonExistingConfirmEmailUserData), MemberType = typeof(UserData))]
    public async Task ConfirmEmail_ReturnUnprocessableEntity_When_UserDoesNotExist(ConfirmEmailDto confirmEmail, string expected)
    {
        FormUrlEncodedContent formContent = ConvertToFormEncodedContent(confirmEmail);

        HttpRequestMessage request = new(HttpMethod.Post, TestApiPathConstants.AccountsConfirmEmail) { Content = formContent };
        HttpResponseMessage actual = await Client.SendAsync(request);
        actual.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        string message = await actual.Content.ReadAsStringAsync();
        message.Should().Contain(expected);

        bool? actualEmailConfirmed = UserStorage.Instance.Data.SingleOrDefault(u => u.Email == confirmEmail.Email)?.EmailConfirmed;
        actualEmailConfirmed.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(UserData.InvalidConfirmEmailTokenData), MemberType = typeof(UserData))]
    public async Task ConfirmEmail_ReturnUnprocessableEntity_When_ToeknIsInvalid(ConfirmEmailDto confirmEmail, string expected)
    {
        UserStorage.Instance.Data.Single(u => u.Email == confirmEmail.Email).EmailConfirmed = false;

        FormUrlEncodedContent formContent = ConvertToFormEncodedContent(confirmEmail);

        HttpRequestMessage request = new(HttpMethod.Post, TestApiPathConstants.AccountsConfirmEmail) { Content = formContent };
        HttpResponseMessage actual = await Client.SendAsync(request);
        actual.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        string message = await actual.Content.ReadAsStringAsync();
        message.Should().Contain(expected);

        bool actualEmailConfirmed = UserStorage.Instance.Data.Single(u => u.Email == confirmEmail.Email).EmailConfirmed;
        actualEmailConfirmed.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserRegistration), MemberType = typeof(UserData))]
    public async Task Login_Should_ReturnOk_When_UserIsValid(UserRegistrationDto userRegistration)
    {
        SimpleSmtpServer smtp = SimpleSmtpServer.Start(EmailData.FakePort);
        int initialCount = UserStorage.Instance.Data.Count;

        await Client.CreateAsync(TestApiPathConstants.AccountsRegister, userRegistration);
        await BackgroundJobHelpers.WaitForCompletion(BackgroundJobType.Email);

        UserStorage.Instance.Data.Last().EmailConfirmed = true;

        UserLoginDto userLogin = UserData.GetUserLogin(userRegistration.Email, userRegistration.Password);
        string? actual = await Client.CreateAndDeserializeAsync<UserLoginDto, string?>(TestApiPathConstants.AccountsLogin, userLogin);

        actual.Should().NotBeNullOrWhiteSpace();
        IDictionary<string, string> tokenInfo = JwtHandler.GetTokenInfo(actual!);

        tokenInfo.ContainsKey(JwtRegisteredClaimNames.Email);
        tokenInfo[JwtRegisteredClaimNames.Email].Should().Be(userRegistration.Email);

        UserStorage.Instance.Data.Should().HaveCount(initialCount + 1);

        smtp.Stop();
    }

    [Theory]
    [MemberData(nameof(UserData.NonExistingUserLoginData), MemberType = typeof(UserData))]
    public async Task Login_Should_ReturnUnauthorized_When_UserDoesNotExist(UserLoginDto userLogin, string expected)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsLogin, userLogin);
        statusCode.Should().Be(HttpStatusCode.Unauthorized);
        message.Should().Contain(expected);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserRegistration), MemberType = typeof(UserData))]
    public async Task Login_Should_ReturnUnauthorized_When_PasswordIsIncorrect(UserRegistrationDto userRegistration)
    {
        SimpleSmtpServer smtp = SimpleSmtpServer.Start(EmailData.FakePort);
        int initialCount = UserStorage.Instance.Data.Count;

        await Client.CreateAsync(TestApiPathConstants.AccountsRegister, userRegistration);
        await BackgroundJobHelpers.WaitForCompletion(BackgroundJobType.Email);

        UserStorage.Instance.Data.Last().EmailConfirmed = true;

        UserLoginDto userLogin = UserData.GetUserLogin(userRegistration.Email, new Faker().Random.String(10));
        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsLogin, userLogin);
        statusCode.Should().Be(HttpStatusCode.Unauthorized);
        message.Should().Contain(AuthConstants.EmailOrPasswordIsIncorrect);

        UserStorage.Instance.Data.Should().HaveCount(initialCount + 1);

        smtp.Stop();
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserLogin), MemberType = typeof(UserData))]
    public async Task Login_ReturnUnauthorized_When_EmailIsNotVerified(UserLoginDto userLogin)
    {
        int initialCount = UserStorage.Instance.Data.Count;
        UserStorage.Instance.Data.Single(u => u.Email == userLogin.Email).EmailConfirmed = false;

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsLogin, userLogin);
        statusCode.Should().Be(HttpStatusCode.Unauthorized);
        message.Should().Contain(AuthConstants.EmailNotVerified);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserLogin), MemberType = typeof(UserData))]
    public async Task Login_Should_ReturnUnauthorized_When_UserIsLocked(UserLoginDto userLogin)
    {
        int initialCount = UserStorage.Instance.Data.Count;
        UserStorage.Instance.Data.Single(u => u.Email == userLogin.Email).LockoutEnd = DateTime.UtcNow.AddMinutes(30);

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsLogin, userLogin);
        statusCode.Should().Be(HttpStatusCode.Unauthorized);
        message.Should().Contain(AuthConstants.UserIsLocked);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);
    }

    [Theory]
    [MemberData(nameof(UserData.ExistingForgotPasswordUser), MemberType = typeof(UserData))]
    public async Task ForgotPassword_Should_ReturnOk_When_UserExists(ForgotPasswordDto forgotPassword)
    {
        SimpleSmtpServer smtp = SimpleSmtpServer.Start(EmailData.FakePort);

        await Client.CreateAsync(TestApiPathConstants.AccountsForgotPassword, forgotPassword);
        await BackgroundJobHelpers.WaitForCompletion(BackgroundJobType.Email);

        SmtpMessage[] emails = smtp.ReceivedEmail;
        emails.Should().HaveCount(1);

        string expected = $"http://localhost:3000/{ApiPathConstants.ResetPassword}?email={forgotPassword.Email}&amp;token={TestConstants.ValidToken.Encode()}";

        SmtpMessage actual = emails[0];
        actual.Subject.Should().Be(AuthConstants.ForgotPasswordSubject);
        actual.MessageParts.Select(mp => mp.BodyData).First().Should().Contain(expected);
        actual.ToAddresses.Should().HaveCount(1);
        actual.ToAddresses[0].Address.Should().Be(forgotPassword.Email);

        smtp.Stop();
    }

    [Theory]
    [MemberData(nameof(UserData.NonExistingForgotPasswordUser), MemberType = typeof(UserData))]
    public async Task ForgotPassword_Should_ReturnOk_When_UserDoesNotExists(ForgotPasswordDto forgotPassword)
    {
        await Client.CreateAsync(TestApiPathConstants.AccountsForgotPassword, forgotPassword);
    }

    [Theory]
    [MemberData(nameof(UserData.ExistingResetPasswordUser), MemberType = typeof(UserData))]
    public async Task ResetPassword_Should_ReturnOk_When_UserExists(ResetPasswordDto resetPassword)
    {
        string expectedPasswordHash = UserStorage.Instance.Data.Single(u => u.Email == resetPassword.Email).PasswordHash;
        FormUrlEncodedContent formContent = ConvertToFormEncodedContent(resetPassword);

        HttpRequestMessage request = new(HttpMethod.Post, TestApiPathConstants.AccountsResetPassword) { Content = formContent };
        HttpResponseMessage actual = await Client.SendAsync(request);
        actual.StatusCode.Should().Be(HttpStatusCode.OK);

        string actualPasswordHash = UserStorage.Instance.Data.Single(u => u.Email == resetPassword.Email).PasswordHash;
        actualPasswordHash.Should().NotBeNullOrWhiteSpace()
            .And.NotBe(expectedPasswordHash);
    }

    [Theory]
    [MemberData(nameof(UserData.NonExistingResetPasswordUserData), MemberType = typeof(UserData))]
    [MemberData(nameof(UserData.InvalidTokenResetPasswordData), MemberType = typeof(UserData))]
    public async Task ResetPassword_Should_ReturnUnprocessableEntity_When_ResetPasswordDataIsInvalid(ResetPasswordDto resetPassword, string expected)
    {
        FormUrlEncodedContent formContent = ConvertToFormEncodedContent(resetPassword);

        HttpRequestMessage request = new(HttpMethod.Post, TestApiPathConstants.AccountsResetPassword) { Content = formContent };
        HttpResponseMessage actual = await Client.SendAsync(request);

        actual.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        string message = await actual.Content.ReadAsStringAsync();
        message.Should().Contain(expected);
    }

    private static void AssertApplicationUser(UserRegistrationDto expected, ApplicationUser actual)
    {
        actual.Id.Should().NotBeEmpty();
        actual.FirstName.Should().Be(expected.FirstName);
        actual.LastName.Should().Be(expected.LastName);
        actual.Email.Should().Be(expected.Email);
        actual.UserName.Should().Be(expected.Email);
        actual.PasswordHash.Should().EndWith("==");
    }

    private static void AssertRole(string expected, IdentityRole<Guid> actual)
    {
        actual.Name.Should().Be(expected);
        actual.NormalizedName.Should().Be(expected.ToUpper(CultureInfo.InvariantCulture));
        actual.ConcurrencyStamp.Should().NotBeEmpty();
    }

    private static void AssertSmtpEmail(UserRegistrationDto expected, SimpleSmtpServer smtp)
    {
        SmtpMessage[] emails = smtp.ReceivedEmail;
        emails.Should().HaveCount(1);

        string expectedUrl = $"http://localhost:3000/{ApiPathConstants.ConfirmEmail}?email={expected.Email}&amp;token={TestConstants.ValidToken.Encode()}";

        SmtpMessage actual = emails[0];
        actual.Subject.Should().Be(AuthConstants.VerifyEmailSubject);
        actual.MessageParts.Select(mp => mp.BodyData).First().Should().Contain(expectedUrl);
        actual.ToAddresses.Should().HaveCount(1);
        actual.ToAddresses[0].Address.Should().Be(expected.Email);
    }

    private static FormUrlEncodedContent ConvertToFormEncodedContent(ResetPasswordDto resetPassword)
    {
        return new(new[]
        {
            new KeyValuePair<string, string>(nameof(ResetPasswordDto.Email), resetPassword.Email),
            new KeyValuePair<string, string>(nameof(ResetPasswordDto.Token), resetPassword.Token),
            new KeyValuePair<string, string>(nameof(ResetPasswordDto.NewPassword), resetPassword.NewPassword),
            new KeyValuePair<string, string>(nameof(ResetPasswordDto.ConfirmPassword), resetPassword.ConfirmPassword),
        });
    }

    private static FormUrlEncodedContent ConvertToFormEncodedContent(ConfirmEmailDto confirmEmail)
    {
        return new(new[]
        {
            new KeyValuePair<string, string>(nameof(ConfirmEmailDto.Email), confirmEmail.Email),
            new KeyValuePair<string, string>(nameof(ConfirmEmailDto.Token), confirmEmail.Token),
        });
    }
}
