// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.Net;
using System.Security.Claims;
using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Shared.Models.Users;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.DataMembers.Storages;
using SocialEventManager.Tests.IntegrationTests.Fixtures;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.ControllerTests;

[Collection(TestConstants.StorageDependent)]
[IntegrationTest]
[Category(CategoryConstants.Identity)]
public sealed class AccountsControllerTests : IntegrationTest
{
    public AccountsControllerTests(ApiWebApplicationFactory fixture)
      : base(fixture)
    {
    }

    [Theory]
    [MemberData(nameof(AccountData.ValidRegisterUserData), MemberType = typeof(AccountData))]
    public async Task Register_Should_ReturnOk_When_UserIsValid(RegisterUserDto user, Account expectedAccount)
    {
        int initialAccountsCount = AccountsStorage.Instance.Data.Count;
        int lastAccountId = AccountsStorage.Instance.Data.LastOrDefault()?.Id ?? 0;
        int lastUserClaimId = UserClaimsStorage.Instance.Data.LastOrDefault()?.Id ?? 0;

        await Client.CreateAsync(ApiPathConstants.RegisterAccounts, user);

        AccountsStorage.Instance.Data.Should().HaveCount(++initialAccountsCount);
        Account actualAccount = AccountsStorage.Instance.Data.Last();
        AssertAccount(expectedAccount, actualAccount, lastAccountId, user.Password, user.ConfirmPassword);

        IEnumerable<UserClaim> expectedUserClaims = GetExpectedUserClaims(lastUserClaimId, actualAccount.UserId, actualAccount.Email);
        IEnumerable<UserClaim> actualUserClaims = UserClaimsStorage.Instance.Data.Where(uc => uc.UserId == actualAccount.UserId);
        actualUserClaims.Should().BeEquivalentTo(expectedUserClaims);
    }

    [Theory]
    [MemberData(nameof(AccountData.InvalidRegisterUserData), MemberType = typeof(AccountData))]
    public async Task Register_Should_ReturnBadRequest_When_UserIsInvalid(RegisterUserDto user, string expected)
    {
        List<Account> initialAccounts = AccountsStorage.Instance.Data;

        (HttpStatusCode statusCode, string message) = await Client.CreateAsyncWithError(ApiPathConstants.RegisterAccounts, user);
        statusCode.Should().Be(HttpStatusCode.BadRequest);
        message.Should().Contain(expected);

        initialAccounts.Should().BeEquivalentTo(AccountsStorage.Instance.Data);
    }

    [Theory]
    [AutoData]
    public async Task Login_Should_ReturnBadRequest_When_UserIsInvalid(LoginModel login)
    {
        (HttpStatusCode statusCode, _) = await Client.CreateAsyncWithError(ApiPathConstants.LoginAccounts, login);
        statusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Login_Should_ReturnInternalServerError_When_PasswordIsWrong()
    {
        LoginModel login = new()
        {
            UserName = AccountsStorage.Instance.Data[0].UserName,
            Password = RandomGeneratorHelpers.GenerateRandomValue(),
        };

        (HttpStatusCode statusCode, _) = await Client.CreateAsyncWithError(ApiPathConstants.LoginAccounts, login);
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task Logout_Should_ReturnOk_When_PasswordIsWrong()
    {
        await Client.CreateAsync(ApiPathConstants.LogoutAccounts);
    }

    private static void AssertAccount(Account expectedAccount, Account actualAccount, int lastAccountId, string password, string confirmPassword)
    {
        actualAccount.Should().BeEquivalentTo(
            expectedAccount,
            opts => opts
                .Excluding(a => a.Id)
                .Excluding(a => a.UserId)
                .Excluding(a => a.PasswordHash)
                .Excluding(a => a.SecurityStamp)
                .Excluding(a => a.ConcurrencyStamp));

        actualAccount.Id.Should().Be(++lastAccountId);
        actualAccount.PasswordHash.Should().NotBe(password)
            .And.NotBe(confirmPassword);
    }

    private static IEnumerable<UserClaim> GetExpectedUserClaims(int lastUserClaimId, Guid userId, string email)
    {
        UserRole userRole = UserRolesStorage.Instance.Data.Single(ur => ur.UserId == userId);
        Role role = RolesStorage.Instance.Data.Single(r => r.Id == userRole.RoleId);

        return new[]
        {
            UserClaimData.GetMockUserClaim(userId, ClaimTypes.Sid, userId.ToString(), ++lastUserClaimId),
            UserClaimData.GetMockUserClaim(userId, ClaimTypes.Email, email, ++lastUserClaimId),
            UserClaimData.GetMockUserClaim(userId, ClaimTypes.Role, role.Name, ++lastUserClaimId),
        };
    }
}
*/
