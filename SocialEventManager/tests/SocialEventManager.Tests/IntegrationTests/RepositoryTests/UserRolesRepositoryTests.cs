using System.Data.SqlClient;
using AutoFixture.Xunit2;
using DeepEqual.Syntax;
using FluentAssertions;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests;

[Collection(TestConstants.DatabaseDependent)]
[IntegrationTest]
[Category(CategoryConstants.Identity)]
public class UserRolesRepositoryTests : RepositoryTestBase<IUserRolesRepository, UserRole>
{
    public UserRolesRepositoryTests(IInMemoryDatabase db, IUserRolesRepository userRolesRepository)
        : base(db, userRolesRepository)
    {
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
    public async Task InsertAsync_Should_ReturnUserRoleId_When_UserRoleIsValid(Account account, Role role)
    {
        await Db.InsertAsync(account);
        await Db.InsertAsync(role);

        int userRoleId = await Repository.InsertAsync(account.UserId, role.Name);
        UserRole actualUserRole = await Db.SingleWhereAsync<UserRole>(nameof(UserRole.Id), userRoleId);

        userRoleId.Should().BeGreaterThan(0);
        actualUserRole.RoleId.Should().Be(role.Id);
        actualUserRole.UserId.Should().Be(account.UserId);
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRole), MemberType = typeof(UserRoleData))]
    public async Task InsertAsync_Should_ThrowSqlException_When_UserIdNotExists(UserRole userRole)
    {
        await Db.InsertAsync(RoleData.GetMockRole(id: userRole.RoleId));

        string foriegnKeyName = $"FK_{nameof(TableNameConstants.UserRoles)}_{nameof(TableNameConstants.Accounts)}_{nameof(UserRole.UserId)}";
        string expectedMessage = ExceptionConstants.ForeignKeyConstraintConflict(foriegnKeyName, TableNameConstants.Accounts, nameof(UserRole.UserId));

        Func<Task> func = async () => await Db.InsertAsync(userRole);
        await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRole), MemberType = typeof(UserRoleData))]
    public async Task InsertAsync_Should_ThrowSqlException_When_RoleIdNotExists(UserRole userRole)
    {
        await Db.InsertAsync(AccountData.GetMockAccount(userRole.UserId));

        string foriegnKeyName = $"FK_{nameof(TableNameConstants.UserRoles)}_{nameof(TableNameConstants.Roles)}_{nameof(UserRole.RoleId)}";
        string expectedMessage = ExceptionConstants.ForeignKeyConstraintConflict(foriegnKeyName, TableNameConstants.Roles, nameof(Role.Id));

        Func<Task> func = async () => await Db.InsertAsync(userRole);
        await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
    }

    [Theory]
    [AutoData]
    public async Task InsertAsync_Should_VerifyNeverCalled_When_UserIdHasDifferentValue(Guid userId, string roleName)
    {
        await MockRepository.Object.InsertAsync(userId, roleName);
        MockRepository.Verify(r => r.InsertAsync(Guid.NewGuid(), roleName), Times.Never);
    }

    [Theory]
    [AutoData]
    public async Task InsertAsync_Should_VerifyCalledOnce_When_UserIdAndRoleNameValuesAreRepeated(Guid userId, string roleName)
    {
        await MockRepository.Object.InsertAsync(userId, roleName);
        MockRepository.Verify(r => r.InsertAsync(userId, roleName), Times.Once);
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRole), MemberType = typeof(UserRoleData))]
    public async Task InsertAsync_Should_ThrowSqlException_When_UserRolesAreDuplicated(UserRole userRole)
    {
        userRole.Id = await CreateUserRoleAndRelatedData(userRole);

        string uniqueConstraintName = $"UC_{nameof(TableNameConstants.UserRoles)}_{nameof(UserRole.UserId)}_{nameof(UserRole.RoleId)}";
        string duplicateKeyValue = $"{userRole.UserId}, {userRole.RoleId}";
        string expectedMessage = ExceptionConstants.ViolationOfUniqueKeyConstraint(uniqueConstraintName, TableNameConstants.UserRoles, duplicateKeyValue);

        Func<Task> func = async () => await Db.InsertAsync(userRole);
        await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
    public async Task GetUserRoles_Should_ReturnUserRoles_When_FilteringByExistingRoleName(Account account, Role role)
    {
        UserRole userRole = await CreateUserRoleAndRelatedData(account, role);

        IEnumerable<UserRole> actualUserRoles = await Repository.GetUserRoles(role.Name);
        actualUserRoles.Should().ContainSingle(actualUserRole => actualUserRole.IsDeepEqual(userRole));
    }

    [Theory]
    [AutoData]
    public async Task GetUserRoles_Should_ReturnEmptyUserRoles_When_FilteringByNonExistingRoleName(string roleName)
    {
        IEnumerable<UserRole> actualUserRoles = await Repository.GetUserRoles(roleName);
        actualUserRoles.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public async Task GetUserRoles_Should_VerifyNeverCalled_When_RoleNameHasDifferentValue(string roleName)
    {
        await MockRepository.Object.GetUserRoles(roleName);
        MockRepository.Verify(r => r.GetUserRoles(null!), Times.Never);
    }

    [Theory]
    [AutoData]
    public async Task GetUserRoles_Should_VerifyCalledOnce_When_RoleNameHasSameValue(string roleName)
    {
        await MockRepository.Object.GetUserRoles(roleName);
        MockRepository.Verify(r => r.GetUserRoles(roleName), Times.Once);
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
    public async Task DeleteUserRole_Should_ReturnTrue_When_UserRoleExists(Account account, Role role)
    {
        await CreateUserRoleAndRelatedData(account, role);

        bool isDeleted = await Repository.DeleteUserRole(account.UserId, role.Name);
        isDeleted.Should().BeTrue();

        bool isInRole = await Repository.IsInRole(account.UserId, role.Name);
        isInRole.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedDataWithMultipleRoles), MemberType = typeof(UserRoleData))]
    public async Task DeleteUserRole_Should_ReturnTrue_When_UserRolesExist(Account account, IEnumerable<Role> roles)
    {
        await Db.InsertAsync(account);
        await Db.InsertAsync(roles);

        UserRole firstUserRole = UserRoleData.GetMockUserRole(roles.First().Id, account.UserId);
        UserRole secondUserRole = UserRoleData.GetMockUserRole(roles.Last().Id, account.UserId);

        await Db.InsertAsync(firstUserRole);
        await Db.InsertAsync(secondUserRole);

        bool isDeleted = await Repository.DeleteUserRole(account.UserId, roles.First().Name);
        isDeleted.Should().BeTrue();

        bool isInRole = await Repository.IsInRole(account.UserId, roles.Last().Name);
        isInRole.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedDataWithMultipleAccounts), MemberType = typeof(UserRoleData))]
    public async Task DeleteUserRole_Should_ReturnTrue_When_UsersRolesExist(IEnumerable<Account> accounts, Role role)
    {
        await Db.InsertAsync(accounts);
        await Db.InsertAsync(role);

        UserRole firstUserRole = UserRoleData.GetMockUserRole(role.Id, accounts.First().UserId);
        UserRole secondUserRole = UserRoleData.GetMockUserRole(role.Id, accounts.Last().UserId);

        await Db.InsertAsync(firstUserRole);
        await Db.InsertAsync(secondUserRole);

        bool isDeleted = await Repository.DeleteUserRole(firstUserRole.UserId, role.Name);
        isDeleted.Should().BeTrue();

        bool isInRole = await Repository.IsInRole(secondUserRole.UserId, role.Name);
        isInRole.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public async Task DeleteUserRole_Should_ReturnFalse_When_UserRolesNotExist(Guid userId, string roleName)
    {
        bool isDeleted = await Repository.DeleteUserRole(userId, roleName);
        isDeleted.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task DeleteUserRole_Should_VerifyNeverCalled_When_RoleNameHasDifferentValue(Guid userId, string roleName)
    {
        await MockRepository.Object.DeleteUserRole(userId, roleName);
        MockRepository.Verify(r => r.DeleteUserRole(userId, null!), Times.Never);
    }

    [Theory]
    [AutoData]
    public async Task DeleteUserRole_Should_VerifyCalledOnce_When_UserIdAndRoleNameValuesAreRepeated(Guid userId, string roleName)
    {
        await MockRepository.Object.DeleteUserRole(userId, roleName);
        MockRepository.Verify(r => r.DeleteUserRole(userId, roleName), Times.Once);
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
    public async Task IsInRole_Should_ReturnTrue_When_UserWithThatRoleExists(Account account, Role role)
    {
        await CreateUserRoleAndRelatedData(account, role);

        bool isInRole = await Repository.IsInRole(account.UserId, role.Name);
        isInRole.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public async Task IsInRole_Should_ReturnFalse_When_UserIdOrRoleNameNotExists(Guid userId, string roleName)
    {
        bool isInRole = await Repository.IsInRole(userId, roleName);
        isInRole.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
    public async Task IsInRole_Should_ReturnFalse_When_UserIdNotExists(Account account, Role role)
    {
        await CreateUserRoleAndRelatedData(account, role);

        bool isInRole = await Repository.IsInRole(Guid.NewGuid(), role.Name);
        isInRole.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
    public async Task IsInRole_Should_ReturnFalse_When_RoleNameNotExists(Account account, Role role)
    {
        await CreateUserRoleAndRelatedData(account, role);

        bool isInRole = await Repository.IsInRole(account.UserId, RandomGeneratorHelpers.GenerateRandomValue());
        isInRole.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task IsInRole_Should_VerifyNeverCalled_When_RoleNameHasDifferentValue(Guid userId, string roleName)
    {
        await MockRepository.Object.IsInRole(userId, roleName);
        MockRepository.Verify(r => r.IsInRole(userId, null!), Times.Never);
    }

    [Theory]
    [AutoData]
    public async Task IsInRole_Should_VerifyCalledOnce_When_UserIdAndRoleNameValuesAreRepeated(Guid userId, string roleName)
    {
        await MockRepository.Object.IsInRole(userId, roleName);
        MockRepository.Verify(r => r.IsInRole(userId, roleName), Times.Once);
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
    public async Task DeleteAsync_Should_DeleteRelatedUserRole_When_AccountIsDeleted(Account account, Role role)
    {
        UserRole userRole = await CreateUserRoleAndRelatedData(account, role);
        await Db.DeleteAsync(account);

        UserRole userRoleAfterDeletion = await Db.SingleWhereAsync<UserRole>(nameof(UserRole.Id), userRole.Id);
        userRoleAfterDeletion.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
    public async Task DeleteAsync_Should_DeleteRelatedUserRole_When_RoleIsDeleted(Account account, Role role)
    {
        UserRole userRole = await CreateUserRoleAndRelatedData(account, role);
        await Db.DeleteAsync(role);

        UserRole userRoleAfterDeletion = await Db.SingleWhereAsync<UserRole>(nameof(UserRole.Id), userRole.Id);
        userRoleAfterDeletion.Should().BeNull();
    }

    #region Private Methods

    private async Task<int> CreateUserRoleAndRelatedData(UserRole userRole)
    {
        await Db.InsertAsync(AccountData.GetMockAccount(userRole.UserId));
        await Db.InsertAsync(RoleData.GetMockRole(id: userRole.RoleId));

        return await Db.InsertAsync(userRole);
    }

    private async Task<UserRole> CreateUserRoleAndRelatedData(Account account, Role role)
    {
        await Db.InsertAsync(account);
        await Db.InsertAsync(role);

        UserRole userRole = UserRoleData.GetMockUserRole(role.Id, account.UserId);
        userRole.Id = await Db.InsertAsync(userRole);

        return userRole;
    }

    #endregion Private Methods
}
