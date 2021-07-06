using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using DeepEqual.Syntax;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Roles;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;
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
    public class ExtendedRepositoryTests : RepositoryTestBase<IRolesRepository, Role>
    {
        public ExtendedRepositoryTests(IInMemoryDatabase db, IRolesRepository rolesRepository)
            : base(db, rolesRepository)
        {
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_ShouldReturnRole(Role role)
        {
            await Db.InsertAsync(role);

            IEnumerable<Role> actualRoles = await Repository.GetAsync(role.Name, nameof(Role.Name));
            AssertHelpers.AssertSingleEqual(role, actualRoles);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_ShouldReturnEmpty(Role role)
        {
            IEnumerable<Role> actualRoles = await Repository.GetAsync(role.Name, nameof(Role.Name));
            Assert.Empty(actualRoles);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByIncompatibleColumnName_ShouldReturnException(Role role)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Repository.GetAsync(role.Name, nameof(Role.Id)));
            Assert.Equal(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier, ex.Message);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_VerifyNeverCalled(Role role)
        {
            await MockRepository.Object.GetAsync(role.Name, nameof(Role.Name));
            MockRepository.Verify(r => r.GetAsync($"Different {role.Name}", nameof(Role.Name)), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_VerifyCalledOnce(Role role)
        {
            await MockRepository.Object.GetAsync(role.Name, nameof(Role.Name));
            MockRepository.Verify(r => r.GetAsync(role.Name, nameof(Role.Name)), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Multiple_ShouldReturnRoles(IEnumerable<Role> roles)
        {
            await Db.InsertAsync(roles);

            IEnumerable<Role> actualRoles = await Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
            Assert.True(actualRoles.OrderBy(r => r.Id).IsDeepEqual(roles.OrderBy(r => r.Id)));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Multiple_ShouldReturnEmpty(IEnumerable<Role> roles)
        {
            IEnumerable<Role> actualRoles = await Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
            Assert.Empty(actualRoles);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByIncompatibleColumnName_Multiple_ShouldReturnException(IEnumerable<Role> roles)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Id)));
            Assert.Equal(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier, ex.Message);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Multiple_VerifyNeverCalled(IEnumerable<Role> roles)
        {
            await MockRepository.Object.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
            MockRepository.Verify(r => r.GetAsync($"Different {roles.Select(role => role.Name)}", nameof(Role.Name)), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Multiple_VerifyCalledOnce(IEnumerable<Role> roles)
        {
            IEnumerable<string> names = roles.Select(role => role.Name);

            await MockRepository.Object.GetAsync(names, nameof(Role.Name));
            MockRepository.Verify(r => r.GetAsync(names, nameof(Role.Name)), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByColumnName_ShouldReturnRole(Role role)
        {
            await Db.InsertAsync(role);

            Role actualRole = await Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
            Assert.True(actualRole.IsDeepEqual(role));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByColumnName_ShouldReturnNull(Role role)
        {
            Role actualRole = await Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
            Assert.Null(actualRole);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByIncompatibleColumnName_ShouldReturnException(Role role)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Id)));
            Assert.Equal(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier, ex.Message);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByColumnName_VerifyNeverCalled(Role role)
        {
            await MockRepository.Object.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
            MockRepository.Verify(r => r.GetSingleOrDefaultAsync($"Different {role.Name}", nameof(Role.Name)), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByColumnName_VerifyCalledOnce(Role role)
        {
            await MockRepository.Object.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
            MockRepository.Verify(r => r.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name)), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_ByColumnName_ShouldReturnTrue(Role role)
        {
            await Db.InsertAsync(role);

            bool isDeleted = await Repository.DeleteAsync(role.Id, nameof(Role.Id));
            Assert.True(isDeleted);

            Role actualRole = await Db.SingleWhereAsync<Role>(nameof(Role.Id), role.Id);
            Assert.Null(actualRole);
        }

        [Fact]
        public async Task DeleteAsync_ByColumnName_ShouldReturnFalse()
        {
            bool isDeleted = await Repository.DeleteAsync(Guid.NewGuid(), nameof(Role.Id));
            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteAsync_ByColumnName_ShouldReturnSqlException()
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Repository.DeleteAsync(RandomGeneratorHelpers.NextInt32(), nameof(Role.Id)));
            Assert.Equal(ExceptionConstants.UniqueIdentifierIsIncompatibleWithInt, ex.Message);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteAsync_ByColumnName_VerifyNeverCalled(Guid roleId)
        {
            await MockRepository.Object.DeleteAsync(roleId, nameof(Role.Id));
            MockRepository.Verify(r => r.DeleteAsync(Guid.NewGuid(), nameof(Role.Id)), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteAsync_ByColumnName_VerifyCalledOnce(Guid roleId)
        {
            await MockRepository.Object.DeleteAsync(roleId, nameof(Role.Id));
            MockRepository.Verify(r => r.DeleteAsync(roleId, nameof(Role.Id)), Times.Once);
        }
    }
}
