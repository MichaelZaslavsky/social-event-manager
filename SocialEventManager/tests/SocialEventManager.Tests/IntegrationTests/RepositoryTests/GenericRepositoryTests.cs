using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests
{
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
        public async Task InsertAsync_Should_Create_Role(Role role)
        {
            await Repository.InsertAsync(role);

            IEnumerable<Role> actualRoles = await Db.WhereAsync<Role>(nameof(role.Id), role.Id);
            actualRoles.Should().ContainSingle(r => r.IsDeepEqual(role));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertAsync_Verify_NeverCalled(Role role)
        {
            await MockRepository.Object.InsertAsync(role);
            MockRepository.Verify(r => r.InsertAsync(new Role()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertAsync_Verify_CalledOnce(Role role)
        {
            await MockRepository.Object.InsertAsync(role);
            MockRepository.Verify(r => r.InsertAsync(role), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task InsertAsync_Multiple_Should_Create_Roles(IEnumerable<Role> roles)
        {
            await Repository.InsertAsync(roles);

            IEnumerable<Role> actualRoles = await Db.SelectAsync<Role>();
            actualRoles.Should().BeEquivalentTo(roles);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task InsertAsync_Multiple_Verify_NeverCalled(IEnumerable<Role> roles)
        {
            await MockRepository.Object.InsertAsync(roles);
            MockRepository.Verify(r => r.InsertAsync(Enumerable.Empty<Role>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task InsertAsync_Multiple_Verify_CalledOnce(IEnumerable<Role> roles)
        {
            await MockRepository.Object.InsertAsync(roles);
            MockRepository.Verify(r => r.InsertAsync(roles), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_Should_Return_Role(Role role)
        {
            await Db.InsertAsync(role);

            Role actualRole = await Repository.GetAsync(role.Id);
            actualRole.Should().BeEquivalentTo(role);
        }

        [Fact]
        public async Task GetAsync_Should_Return_Null()
        {
            Role actualRole = await Repository.GetAsync(Guid.NewGuid());
            actualRole.Should().BeNull();
        }

        [Theory]
        [InlineAutoData]
        public async Task GetAsync_Verify_NeverCalled(Guid roleId)
        {
            await MockRepository.Object.GetAsync(roleId);
            MockRepository.Verify(r => r.GetAsync(Guid.NewGuid()), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetAsync_Verify_CalledOnce(Guid roleId)
        {
            await MockRepository.Object.GetAsync(roleId);
            MockRepository.Verify(r => r.GetAsync(roleId), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_Multiple_Should_Return_Roles(IEnumerable<Role> roles)
        {
            await Db.InsertAsync(roles);

            IEnumerable<Role> actualRoles = await Repository.GetAsync();
            actualRoles.Should().BeEquivalentTo(roles);
        }

        [Fact]
        public async Task GetAsync_Multiple_Should_Return_Empty_Roles()
        {
            IEnumerable<Role> actualRoles = await Repository.GetAsync();
            actualRoles.Should().BeEmpty();
        }

        [Fact]
        public void GetAsync_Multiple_Verify_NeverCalled()
        {
            MockRepository.Verify(r => r.GetAsync(), Times.Never);
        }

        [Fact]
        public async Task GetAsync_Multiple_Verify_CalledOnce()
        {
            await MockRepository.Object.GetAsync();
            MockRepository.Verify(r => r.GetAsync(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task UpdateAsync_Should_Return_True(Role role)
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
        public async Task UpdateAsync_Should_Return_False(Role role)
        {
            bool isUpdated = await Repository.UpdateAsync(role);
            isUpdated.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task UpdateAsync_Verify_NeverCalled(Role role)
        {
            await MockRepository.Object.UpdateAsync(role);
            MockRepository.Verify(r => r.UpdateAsync(null), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task UpdateAsync_Verify_CalledOnce(Role role)
        {
            await MockRepository.Object.UpdateAsync(role);
            MockRepository.Verify(r => r.UpdateAsync(role), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_Should_Return_True(Role role)
        {
            await Db.InsertAsync(role);

            bool isDeleted = await Repository.DeleteAsync(role);
            isDeleted.Should().BeTrue();

            Role actualRole = await Db.SingleWhereAsync<Role>(nameof(role.Id), role.Id);
            actualRole.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_Should_Return_False(Role role)
        {
            bool isDeleted = await Repository.DeleteAsync(role);
            isDeleted.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_Verify_NeverCalled(Role role)
        {
            await MockRepository.Object.DeleteAsync(role);
            MockRepository.Verify(r => r.DeleteAsync(null), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_Verify_CalledOnce(Role role)
        {
            await MockRepository.Object.DeleteAsync(role);
            MockRepository.Verify(r => r.DeleteAsync(role), Times.Once);
        }

        [Fact]
        public void InitializeConstructorWithNullDbSession_Should_Return_SqlException()
        {
            Action action = () => new RolesRepository(null);
            action.Should().Throw<ArgumentNullException>().WithMessage(ExceptionConstants.ValueCannotBeNull("session"));
        }
    }
}
