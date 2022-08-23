using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using netDumbster.smtp;
using SocialEventManager.Infrastructure.Auth;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Shared.Models.Auth;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.DataMembers.Identity;
using SocialEventManager.Tests.Common.DataMembers.Storages.Identity;
using SocialEventManager.Tests.Common.Helpers;
using SocialEventManager.Tests.IntegrationTests.Fixtures;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests;

[Collection(TestConstants.StorageDependent)]
[IntegrationTest]
[Category(CategoryConstants.Identity)]
public class AccountsControllerTests : IntegrationTest
{
    public AccountsControllerTests(ApiWebApplicationFactory fixture, IJwtHandler jwtHandler)
      : base(fixture, jwtHandler)
    {
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserRegistration), MemberType = typeof(UserData))]
    public async Task Register_ReturnsOk_WhenUserIsValid(UserRegistrationDto userRegistration)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        await Client.CreateAsync(TestApiPathConstants.AccountsRegister, userRegistration);

        UserStorage.Instance.Data.Should().HaveCount(initialCount + 1);

        ApplicationUser actual = UserStorage.Instance.Data.Last();
        AssertApplicationUser(userRegistration, actual);

        UserRoleStorage.Instance.Data.Should().ContainSingle(ur => ur.UserId == actual.Id);
        IdentityUserRole<Guid> actualUserRole = UserRoleStorage.Instance.Data.Single(ur => ur.UserId == actual.Id);
        IdentityRole<Guid> actualRole = RoleStorage.Instance.Data.Single(r => r.Id == actualUserRole.RoleId);
        AssertRole(UserRoles.User, actualRole);
    }

    [Theory]
    [MemberData(nameof(UserData.ExistingUserRegistrationData), MemberType = typeof(UserData))]
    [MemberData(nameof(UserData.InvalidUserRegistrationData), MemberType = typeof(UserData))]
    public async Task Register_ReturnsBadRequest_WhenUserIsInvalid(UserRegistrationDto userRegistration, string expected)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsRegister, userRegistration);
        statusCode.Should().Be(HttpStatusCode.BadRequest);
        message.Should().Contain(expected);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserRegistration), MemberType = typeof(UserData))]
    public async Task Login_ReturnsOk_WhenUserIsValid(UserRegistrationDto userRegistration)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        await Client.CreateAsync(TestApiPathConstants.AccountsRegister, userRegistration);

        UserLoginDto userLogin = UserData.GetUserLogin(userRegistration.Email, userRegistration.Password);
        string? actual = await Client.CreateAndDeserializeAsync<UserLoginDto, string?>(TestApiPathConstants.AccountsLogin, userLogin);

        UserStorage.Instance.Data.Should().HaveCount(initialCount + 1);

        actual.Should().NotBeNullOrWhiteSpace();
        IDictionary<string, string> tokenInfo = JwtHandler.GetTokenInfo(actual!);

        tokenInfo.ContainsKey(JwtRegisteredClaimNames.Email);
        tokenInfo[JwtRegisteredClaimNames.Email].Should().Be(userRegistration.Email);
    }

    [Theory]
    [MemberData(nameof(UserData.NonExistingUserLoginData), MemberType = typeof(UserData))]
    public async Task Login_ReturnsUnauthorized_WhenUserDoesNotExist(UserLoginDto userLogin, string expected)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsLogin, userLogin);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);

        statusCode.Should().Be(HttpStatusCode.Unauthorized);
        message.Should().Contain(expected);
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserRegistration), MemberType = typeof(UserData))]
    public async Task Login_ReturnsUnauthorized_WhenPasswordIsIncorrect(UserRegistrationDto userRegistration)
    {
        int initialCount = UserStorage.Instance.Data.Count;

        await Client.CreateAsync(TestApiPathConstants.AccountsRegister, userRegistration);

        UserLoginDto userLogin = UserData.GetUserLogin(userRegistration.Email, RandomGeneratorHelpers.GenerateRandomValue());
        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsLogin, userLogin);

        UserStorage.Instance.Data.Should().HaveCount(initialCount + 1);

        statusCode.Should().Be(HttpStatusCode.Unauthorized);
        message.Should().Contain(AuthConstants.EmailOrPasswordIsIncorrect);
    }

    [Theory]
    [MemberData(nameof(UserData.ValidUserLogin), MemberType = typeof(UserData))]
    public async Task Login_ReturnsUnauthorized_WhenUserIsLocked(UserLoginDto userLogin)
    {
        int initialCount = UserStorage.Instance.Data.Count;
        UserStorage.Instance.Data.Single(u => u.Email == userLogin.Email).LockoutEnd = DateTime.UtcNow.AddMinutes(30);

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(TestApiPathConstants.AccountsLogin, userLogin);

        UserStorage.Instance.Data.Should().HaveCount(initialCount);

        statusCode.Should().Be(HttpStatusCode.Unauthorized);
        message.Should().Contain(AuthConstants.UserIsLocked);
    }

    [Theory]
    [MemberData(nameof(UserData.ExistingForgotPasswordUser), MemberType = typeof(UserData))]
    public async Task ForgotPassword_ReturnsOk_WhenUserExists(ForgotPasswordDto forgotPassword)
    {
        SimpleSmtpServer smtp = SimpleSmtpServer.Start(EmailData.FakePort);

        await Client.CreateAsync(TestApiPathConstants.AccountsForgotPassword, forgotPassword);
        await BackgroundJobHelpers.WaitForCompletion(BackgroundJobType.Email);

        SmtpMessage[] emails = smtp.ReceivedEmail;
        emails.Should().HaveCount(1);

        string expected = $"http://localhost:3000/reset-password?email=valid-email@email-domain.com&token={TestConstants.ValidToken.Encode()}";

        SmtpMessage actual = emails[0];
        actual.Subject.Should().Be(AuthConstants.ForgotPasswordSubject);
        actual.MessageParts.Select(mp => mp.BodyData).First().Should().Be(AuthConstants.ForgotPasswordBody(TestConstants.SomeText, expected));
        actual.ToAddresses.Should().HaveCount(1);
        actual.ToAddresses[0].Address.Should().Be(forgotPassword.Email);

        smtp.Stop();
    }

    [Theory]
    [MemberData(nameof(UserData.NonExistingForgotPasswordUser), MemberType = typeof(UserData))]
    public async Task ForgotPassword_ReturnsOk_WhenUserDoesNotExists(ForgotPasswordDto forgotPassword)
    {
        await Client.CreateAsync(TestApiPathConstants.AccountsForgotPassword, forgotPassword);
    }

    [Theory]
    [MemberData(nameof(UserData.ExistingResetPasswordUser), MemberType = typeof(UserData))]
    public async Task ResetPassword_ReturnsOk_WhenUserExists(ResetPasswordDto resetPassword)
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
    public async Task ResetPassword_ReturnsBadRequest_WhenResetPasswordDataIsInvalid(ResetPasswordDto resetPassword, string expected)
    {
        FormUrlEncodedContent formContent = ConvertToFormEncodedContent(resetPassword);

        HttpRequestMessage request = new(HttpMethod.Post, TestApiPathConstants.AccountsResetPassword) { Content = formContent };
        HttpResponseMessage actual = await Client.SendAsync(request);

        actual.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
}
