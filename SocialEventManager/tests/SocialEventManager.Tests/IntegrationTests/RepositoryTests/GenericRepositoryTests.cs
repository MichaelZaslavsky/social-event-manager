using AutoFixture.Xunit2;
using DeepEqual.Syntax;
using FluentAssertions;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests;

// Test GenericRepository through RolesRepository
[Collection(DataConstants.RepositoryTests)]
[IntegrationTest]
[Category(CategoryConstants.Infrastructure)]
public class GenericRepositoryTests : RepositoryTestBase<IRolesRepository, Role>
{
    public GenericRepositoryTests(IInMemoryDatabase db, IRolesRepository rolesRepository)
        : base(db, rolesRepository)
    {
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_CreateRole_When_RoleIsValid(Role role)
    {
        await Repository.InsertAsync(role);

        IEnumerable<Role> actualRoles = await Db.WhereAsync<Role>(nameof(role.Id), role.Id);
        actualRoles.Should().ContainSingle(r => r.IsDeepEqual(role));
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_VerifyNeverCalled_When_RoleHasDifferentValue(Role role)
    {
        await MockRepository.Object.InsertAsync(role);
        MockRepository.Verify(r => r.InsertAsync(new Role()), Times.Never);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_VerifyCalledOnce_When_RoleHasSameValue(Role role)
    {
        await MockRepository.Object.InsertAsync(role);
        MockRepository.Verify(r => r.InsertAsync(role), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_CreateRoles_When_RolesAreValid(IEnumerable<Role> roles)
    {
        await Repository.InsertAsync(roles);

        IEnumerable<Role> actualRoles = await Db.SelectAsync<Role>();
        actualRoles.Should().BeEquivalentTo(roles);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_VerifyNeverCalled_When_RolesHaveDifferentValue(IEnumerable<Role> roles)
    {
        await MockRepository.Object.InsertAsync(roles);
        MockRepository.Verify(r => r.InsertAsync(Enumerable.Empty<Role>()), Times.Never);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task InsertAsync_Should_VerifyCalledOnce_When_RolesHaveSameValue(IEnumerable<Role> roles)
    {
        await MockRepository.Object.InsertAsync(roles);
        MockRepository.Verify(r => r.InsertAsync(roles), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_ReturnRole_When_FilteringByExistingRoleId(Role role)
    {
        await Db.InsertAsync(role);

        Role actualRole = await Repository.GetAsync(role.Id);
        actualRole.Should().BeEquivalentTo(role);
    }

    [Fact]
    public async Task GetAsync_Should_ReturnNull_When_FilteringByNonExistingRoleId()
    {
        Role actualRole = await Repository.GetAsync(Guid.NewGuid());
        actualRole.Should().BeNull();
    }

    [Theory]
    [InlineAutoData]
    public async Task GetAsync_Should_VerifyNeverCalled_When_RoleIdHasDifferentValue(Guid roleId)
    {
        await MockRepository.Object.GetAsync(roleId);
        MockRepository.Verify(r => r.GetAsync(Guid.NewGuid()), Times.Never);
    }

    [Theory]
    [InlineAutoData]
    public async Task GetAsync_Should_VerifyCalledOnce_When_RoleIdHasSameValue(Guid roleId)
    {
        await MockRepository.Object.GetAsync(roleId);
        MockRepository.Verify(r => r.GetAsync(roleId), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
    public async Task GetAsync_Should_ReturnRoles_When_RolesExist(IEnumerable<Role> roles)
    {
        await Db.InsertAsync(roles);

        IEnumerable<Role> actualRoles = await Repository.GetAsync();
        actualRoles.Should().BeEquivalentTo(roles);
    }

    [Fact]
    public async Task GetAsync_Should_ReturnEmptyRoles_When_RolesNotExist()
    {
        IEnumerable<Role> actualRoles = await Repository.GetAsync();
        actualRoles.Should().BeEmpty();
    }

    [Fact]
    public void GetAsync_Should_VerifyNeverCalled_When_MethodWasNotCalled()
    {
        MockRepository.Verify(r => r.GetAsync(), Times.Never);
    }

    [Fact]
    public async Task GetAsync_Should_VerifyCalledOnce_When_MethodCalledOnce()
    {
        await MockRepository.Object.GetAsync();
        MockRepository.Verify(r => r.GetAsync(), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task UpdateAsync_Should_ReturnTrue_When_RoleIsValid(Role role)
    {
        await Db.InsertAsync(role);

        role.Name = $"Updated: {role.Name}";

        bool isUpdated = await Repository.UpdateAsync(role);
        isUpdated.Should().BeTrue();

        Role actualRole = await Db.SingleWhereAsync<Role>(nameof(Role.Id), role.Id);
        actualRole.Should().BeEquivalentTo(role);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task UpdateAsync_Should_ReturnFalse_When_RoleNotExists(Role role)
    {
        bool isUpdated = await Repository.UpdateAsync(role);
        isUpdated.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task UpdateAsync_Should_VerifyNeverCalled_When_RoleHasDifferentValue(Role role)
    {
        await MockRepository.Object.UpdateAsync(role);
        MockRepository.Verify(r => r.UpdateAsync(null!), Times.Never);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task UpdateAsync_Should_VerifyCalledOnce_When_RoleHasSameValue(Role role)
    {
        await MockRepository.Object.UpdateAsync(role);
        MockRepository.Verify(r => r.UpdateAsync(role), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task DeleteAsync_Should_ReturnTrue_When_RoleExists(Role role)
    {
        await Db.InsertAsync(role);

        bool isDeleted = await Repository.DeleteAsync(role);
        isDeleted.Should().BeTrue();

        Role actualRole = await Db.SingleWhereAsync<Role>(nameof(role.Id), role.Id);
        actualRole.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task DeleteAsync_Should_ReturnFalse_When_RoleNotExists(Role role)
    {
        bool isDeleted = await Repository.DeleteAsync(role);
        isDeleted.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task DeleteAsync_Should_VerifyNeverCalled_When_RoleHasDifferentValue(Role role)
    {
        await MockRepository.Object.DeleteAsync(role);
        MockRepository.Verify(r => r.DeleteAsync(null!), Times.Never);
    }

    [Theory]
    [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
    public async Task DeleteAsync_Should_VerifyCalledOnce_When_RoleHasSameValue(Role role)
    {
        await MockRepository.Object.DeleteAsync(role);
        MockRepository.Verify(r => r.DeleteAsync(role), Times.Once);
    }

    [Fact]
    public void InitializeConstructor_Should_ThrowSqlException_When_DbSessionIsNull()
    {
        Action action = () => new RolesRepository(null!);
        action.Should().Throw<ArgumentNullException>().WithMessage(ExceptionConstants.ValueCannotBeNull("session"));
    }
}
