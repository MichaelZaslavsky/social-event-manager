using System.Data.SqlClient;
using AutoFixture.Xunit2;
using DeepEqual.Syntax;
using FluentAssertions;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests;

// Test GenericRepository through RolesRepository
[Collection(DataConstants.RepositoryTests)]
[IntegrationTest]
[Category(CategoryConstants.Infrastructure)]
public class ExtendedRepositoryTests : RepositoryTestBase<IRolesRepository, Role>
{
    public ExtendedRepositoryTests(IInMemoryDatabase db, IRolesRepository rolesRepository)
        : base(db, rolesRepository)
    {
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_ReturnRoles_When_FilteringByExistingRoleName(Role role)
    {
        await Db.InsertAsync(role);

        IEnumerable<Role> actualRoles = await Repository.GetAsync(role.Name, nameof(Role.Name));
        actualRoles.Should().ContainSingle(r => r.IsDeepEqual(role));
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_ReturnEmptyRoles_When_FilteringByNonExistingRoleName(Role role)
    {
        IEnumerable<Role> actualRoles = await Repository.GetAsync(role.Name, nameof(Role.Name));
        actualRoles.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_ThrowSqlException_When_FilteringByIncompatibleColumnName(Role role)
    {
        Func<Task> func = async () => await Repository.GetAsync(role.Name, nameof(Role.Id));
        await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_VerifyNeverCalled_When_RoleNameHasDifferentValue(Role role)
    {
        await MockRepository.Object.GetAsync(role.Name, nameof(Role.Name));
        MockRepository.Verify(r => r.GetAsync($"Different {role.Name}", nameof(Role.Name)), Times.Never);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_VerifyCalledOnce_When_RoleNameHasSameValue(Role role)
    {
        await MockRepository.Object.GetAsync(role.Name, nameof(Role.Name));
        MockRepository.Verify(r => r.GetAsync(role.Name, nameof(Role.Name)), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_ReturnRoles_When_FilteringByExistingRoleNames(IEnumerable<Role> roles)
    {
        await Db.InsertAsync(roles);

        IEnumerable<Role> actualRoles = await Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
        actualRoles.Should().BeEquivalentTo(roles);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_ReturnEmptyRoles_When_FilteringByNonExistingRoleNames(IEnumerable<Role> roles)
    {
        IEnumerable<Role> actualRoles = await Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
        actualRoles.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_ThrowSqlException_When_FilteringByIncompatibleColumnNames(IEnumerable<Role> roles)
    {
        Func<Task> func = async () => await Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Id));
        await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_VerifyNeverCalled_When_RoleNamesHaveDifferentValues(IEnumerable<Role> roles)
    {
        await MockRepository.Object.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
        MockRepository.Verify(r => r.GetAsync($"Different {roles.Select(role => role.Name)}", nameof(Role.Name)), Times.Never);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_VerifyCalledOnce_When_RoleNamesHaveSameValues(IEnumerable<Role> roles)
    {
        IEnumerable<string> names = roles.Select(role => role.Name);

        await MockRepository.Object.GetAsync(names, nameof(Role.Name));
        MockRepository.Verify(r => r.GetAsync(names, nameof(Role.Name)), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetSingleOrDefaultAsync_Should_ReturnRole_When_FilteringByExistingRoleName(Role role)
    {
        await Db.InsertAsync(role);

        Role? actualRole = await Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
        actualRole.Should().BeEquivalentTo(role);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetSingleOrDefaultAsync_Should_ReturnNull_When_FilteringByNonExistingRoleName(Role role)
    {
        Role? actualRole = await Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
        actualRole.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetSingleOrDefaultAsync_Should_ThrowSqlException_When_FilteringByIncompatibleColumnName(Role role)
    {
        Func<Task> func = async () => await Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Id));
        await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetSingleOrDefaultAsync_Should_VerifyNeverCalled_When_RoleNameHasDifferentValue(Role role)
    {
        await MockRepository.Object.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
        MockRepository.Verify(r => r.GetSingleOrDefaultAsync($"Different {role.Name}", nameof(Role.Name)), Times.Never);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetSingleOrDefaultAsync_Should_VerifyCalledOnce_When_RoleNameHasSameValue(Role role)
    {
        await MockRepository.Object.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
        MockRepository.Verify(r => r.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name)), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task DeleteAsync_Should_ReturnTrue_When_RoleIdExists(Role role)
    {
        await Db.InsertAsync(role);

        bool isDeleted = await Repository.DeleteAsync(role.Id, nameof(Role.Id));
        isDeleted.Should().BeTrue();

        Role actualRole = await Db.SingleWhereAsync<Role>(nameof(Role.Id), role.Id);
        actualRole.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Should_ReturnFalse_When_RoleIdNotExists()
    {
        bool isDeleted = await Repository.DeleteAsync(Guid.NewGuid(), nameof(Role.Id));
        isDeleted.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_Should_ThrowSqlException_When_RoleIdIsInvalid()
    {
        Func<Task> func = async () => await Repository.DeleteAsync(RandomGeneratorHelpers.NextInt32(), nameof(Role.Id));
        await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.UniqueIdentifierIsIncompatibleWithInt);
    }

    [Theory]
    [InlineAutoData]
    public async Task DeleteAsync_Should_VerifyNeverCalled_When_RoleIdHasDifferentValue(Guid roleId)
    {
        await MockRepository.Object.DeleteAsync(roleId, nameof(Role.Id));
        MockRepository.Verify(r => r.DeleteAsync(Guid.NewGuid(), nameof(Role.Id)), Times.Never);
    }

    [Theory]
    [InlineAutoData]
    public async Task DeleteAsync_Should_VerifyCalledOnce_When_RoleIdHasSameValue(Guid roleId)
    {
        await MockRepository.Object.DeleteAsync(roleId, nameof(Role.Id));
        MockRepository.Verify(r => r.DeleteAsync(roleId, nameof(Role.Id)), Times.Once);
    }
}
