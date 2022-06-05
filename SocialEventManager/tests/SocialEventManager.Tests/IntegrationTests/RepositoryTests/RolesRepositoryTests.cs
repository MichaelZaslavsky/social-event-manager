using System.Data.SqlClient;
using AutoFixture.Xunit2;
using DeepEqual.Syntax;
using FluentAssertions;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests;

[Collection(DataConstants.RepositoryTests)]
[IntegrationTest]
[Category(CategoryConstants.Identity)]
public class RolesRepositoryTests : RepositoryTestBase<IRolesRepository, Role>
{
    public RolesRepositoryTests(IInMemoryDatabase db, IRolesRepository rolesRepository)
        : base(db, rolesRepository)
    {
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task InsertRole_Should_ReturnRoleId_When_RoleIsValid(Role role)
    {
        Guid roleId = await Repository.InsertRole(role);

        Role actualRole = await Db.SingleWhereAsync<Role>(nameof(role.Id), roleId);
        actualRole.Should().BeEquivalentTo(role);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task InsertRole_Should_VerifyNeverCalled_When_RoleHasDifferentValue(Role role)
    {
        await MockRepository.Object.InsertRole(role);
        MockRepository.Verify(r => r.InsertRole(null!), Times.Never);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task InsertRole_Should_VerifyCalledOnce_When_RoleHasSameValue(Role role)
    {
        await MockRepository.Object.InsertRole(role);
        MockRepository.Verify(r => r.InsertRole(role), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_ThrowSqlException_When_RoleIsDuplicated(Role role)
    {
        await Db.InsertAsync(role);

        Func<Task> func = async () => await Db.InsertAsync(role);
        string message = (await func.Should().ThrowAsync<SqlException>()).Subject.First().Message;
        message.Should().StartWith(ExceptionConstants.ViolationOfPrimaryKeyConstraint($"PK__{AliasConstants.Roles}__"));
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_ThrowSqlException_When_RoleIdIsDuplicated(Role role)
    {
        await Db.InsertAsync(role);

        Role duplicatedIdRole = RoleData.GetMockRole(RandomGeneratorHelpers.GenerateRandomValue(), role.Id);

        Func<Task> func = async () => await Db.InsertAsync(duplicatedIdRole);
        string message = (await func.Should().ThrowAsync<SqlException>()).Subject.First().Message;
        message.Should().StartWith(ExceptionConstants.ViolationOfPrimaryKeyConstraint($"PK__{AliasConstants.Roles}__"));
    }

    [Theory]
    [MemberData(nameof(RoleData.RolesWithSameName), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_ThrowSqlException_When_RoleNameIsDuplicated(IEnumerable<Role> roles)
    {
        string uniqueConstraintName = $"UC_{AliasConstants.Roles}_{nameof(Role.Name)}";
        string duplicateKeyValue = roles.First().Name;

        Func<Task> func = async () => await Db.InsertAsync(roles);
        await func.Should().ThrowAsync<SqlException>()
            .WithMessage(ExceptionConstants.ViolationOfUniqueKeyConstraint(uniqueConstraintName, TableNameConstants.Roles, duplicateKeyValue));
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetByUserIdAsync_Should_ReturnRoles_When_FilteringByExistingUserWithRole(Role role)
    {
        await Db.CreateTableIfNotExistsAsync<Account>();
        await Db.CreateTableIfNotExistsAsync<UserRole>();

        Account account = AccountData.GetMockAccount();
        UserRole userRole = UserRoleData.GetMockUserRole(role.Id, account.UserId);

        await Db.InsertAsync(account);
        await Db.InsertAsync(role);
        await Db.InsertAsync(userRole);

        IEnumerable<Role> actualRoles = await Repository.GetByUserIdAsync(userRole.UserId);
        actualRoles.Should().ContainSingle(actualRole => actualRole.IsDeepEqual(role));
    }

    [Fact]
    public async Task GetByUserIdAsync_Should_ReturnEmptyRoles_When_FilteringByNonExistingUser()
    {
        await Db.CreateTableIfNotExistsAsync<Account>();
        await Db.CreateTableIfNotExistsAsync<UserRole>();

        IEnumerable<Role> actualRoles = await Repository.GetByUserIdAsync(Guid.NewGuid());
        actualRoles.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByUserIdAsync_Should_ThrowSqlException_When_UserRoleTableWasNotCreated()
    {
        string tableName = SqlMapperUtilities.GetTableName<UserRole>();

        Func<Task> func = async () => await Repository.GetByUserIdAsync(Guid.NewGuid());
        await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.InvalidObjectName(tableName));
    }

    [Theory]
    [InlineAutoData]
    public async Task GetByUserIdAsync_Should_VerifyNeverCalled_When_UserIdHasDifferentValue(Guid userId)
    {
        await MockRepository.Object.GetByUserIdAsync(userId);
        MockRepository.Verify(r => r.GetByUserIdAsync(Guid.NewGuid()), Times.Never);
    }

    [Theory]
    [InlineAutoData]
    public async Task GetByUserIdAsync_Should_VerifyCalledOnce_When_UserIdHasSameValue(Guid userId)
    {
        await MockRepository.Object.GetByUserIdAsync(userId);
        MockRepository.Verify(r => r.GetByUserIdAsync(userId), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.RoleWithValidLength), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_Succeed_When_RoleIsValid(Role role)
    {
        await Db.InsertAsync(role);
    }

    [Theory]
    [MemberData(nameof(RoleData.RoleWithMissingRequiredFields), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_ThrowSqlException_When_RequiredFieldsAreMissing(Role role, string expectedMessage)
    {
        Func<Task> func = async () => await Db.InsertAsync(role);
        (await func.Should().ThrowAsync<SqlException>()).WithMessage(expectedMessage);
    }

    [Theory]
    [MemberData(nameof(RoleData.RoleWithExceededLength), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_ThrowSqlException_When_LengthIsExceeded(Role role, string expectedMessage)
    {
        Func<Task> func = async () => await Db.InsertAsync(role);
        (await func.Should().ThrowAsync<SqlException>()).And.Message.Should().StartWith(expectedMessage);
    }
}
