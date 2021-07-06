using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using DeepEqual.Syntax;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.Helpers;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests
{
    // Test GenericRepository through RolesRepository
    [Collection(nameof(RolesRepositoryTests))]
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
        public async Task InsertAsync(Role role)
        {
            await Repository.InsertAsync(role);

            IEnumerable<Role> actualRoles = await Db.WhereAsync<Role>(nameof(role.Id), role.Id);
            AssertHelpers.AssertSingleEqual(role, actualRoles);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertAsync_VerifyNeverCalled(Role role)
        {
            await MockRepository.Object.InsertAsync(role);
            MockRepository.Verify(r => r.InsertAsync(new Role()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertAsync_VerifyCalledOnce(Role role)
        {
            await MockRepository.Object.InsertAsync(role);
            MockRepository.Verify(r => r.InsertAsync(role), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task InsertAsync_Multiple(IEnumerable<Role> roles)
        {
            await Repository.InsertAsync(roles);

            IEnumerable<Role> actualRoles = await Db.SelectAsync<Role>();
            Assert.True(actualRoles.OrderBy(r => r.Id).IsDeepEqual(roles.OrderBy(r => r.Id)));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task InsertAsync_Multiple_VerifyNeverCalled(IEnumerable<Role> roles)
        {
            await MockRepository.Object.InsertAsync(roles);
            MockRepository.Verify(r => r.InsertAsync(Enumerable.Empty<Role>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task InsertAsync_Multiple_VerifyCalledOnce(IEnumerable<Role> roles)
        {
            await MockRepository.Object.InsertAsync(roles);
            MockRepository.Verify(r => r.InsertAsync(roles), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ShouldReturnRole(Role role)
        {
            await Db.InsertAsync(role);

            Role actualRole = await Repository.GetAsync(role.Id);
            Assert.True(actualRole.IsDeepEqual(role));
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull()
        {
            Role actualRole = await Repository.GetAsync(Guid.NewGuid());
            Assert.Null(actualRole);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetAsync_VerifyNeverCalled(Guid roleId)
        {
            await MockRepository.Object.GetAsync(roleId);
            MockRepository.Verify(r => r.GetAsync(Guid.NewGuid()), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetAsync_VerifyCalledOnce(Guid roleId)
        {
            await MockRepository.Object.GetAsync(roleId);
            MockRepository.Verify(r => r.GetAsync(roleId), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_Multiple_ShouldReturnRoles(IEnumerable<Role> roles)
        {
            await Db.InsertAsync(roles);

            IEnumerable<Role> actualRoles = await Repository.GetAsync();
            Assert.True(actualRoles.OrderBy(r => r.Id).IsDeepEqual(roles.OrderBy(r => r.Id)));
        }

        [Fact]
        public async Task GetAsync_Multiple_ShouldReturnEmpty()
        {
            IEnumerable<Role> actualRoles = await Repository.GetAsync();
            Assert.Empty(actualRoles);
        }

        [Fact]
        public void GetAsync_Multiple_VerifyNeverCalled()
        {
            MockRepository.Verify(r => r.GetAsync(), Times.Never);
        }

        [Fact]
        public async Task GetAsync_Multiple_VerifyCalledOnce()
        {
            await MockRepository.Object.GetAsync();
            MockRepository.Verify(r => r.GetAsync(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task UpdateAsync_ShouldReturnTrue(Role role)
        {
            await Db.InsertAsync(role);

            role.Name = $"Updated: {role.Name}";

            bool isUpdated = await Repository.UpdateAsync(role);
            Assert.True(isUpdated);

            Role actualRole = await Db.SingleWhereAsync<Role>(nameof(Role.Id), role.Id);
            Assert.True(actualRole.IsDeepEqual(role));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task UpdateAsync_ShouldReturnFalse(Role role)
        {
            bool isUpdated = await Repository.UpdateAsync(role);
            Assert.False(isUpdated);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task UpdateAsync_VerifyNeverCalled(Role role)
        {
            await MockRepository.Object.UpdateAsync(role);
            MockRepository.Verify(r => r.UpdateAsync(null), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task UpdateAsync_VerifyCalledOnce(Role role)
        {
            await MockRepository.Object.UpdateAsync(role);
            MockRepository.Verify(r => r.UpdateAsync(role), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_ShouldReturnTrue(Role role)
        {
            await Db.InsertAsync(role);

            bool isDeleted = await Repository.DeleteAsync(role);
            Assert.True(isDeleted);

            Role actualRole = await Db.SingleWhereAsync<Role>(nameof(role.Id), role.Id);
            Assert.Null(actualRole);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_ShouldReturnFalse(Role role)
        {
            bool isDeleted = await Repository.DeleteAsync(role);
            Assert.False(isDeleted);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_VerifyNeverCalled(Role role)
        {
            await MockRepository.Object.DeleteAsync(role);
            MockRepository.Verify(r => r.DeleteAsync(null), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_VerifyCalledOnce(Role role)
        {
            await MockRepository.Object.DeleteAsync(role);
            MockRepository.Verify(r => r.DeleteAsync(role), Times.Once);
        }
    }
}
