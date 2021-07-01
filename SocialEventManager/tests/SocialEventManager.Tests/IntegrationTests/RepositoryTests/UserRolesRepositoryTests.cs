using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Users;
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
    [Collection(nameof(RolesRepositoryTests))]
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
        public async Task InsertAsync(Account account, Role role)
        {
            await Db.InsertAsync(account);
            await Db.InsertAsync(role);

            int userRoleId = await Repository.InsertAsync(account.UserId, role.Name);
            UserRole actualUserRole = await Db.SingleWhereAsync<UserRole>(nameof(UserRole.Id), userRoleId);

            Assert.True(userRoleId > 0);
            Assert.Equal(role.Id, actualUserRole.RoleId);
            Assert.Equal(account.UserId, actualUserRole.UserId);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task InsertAsync_NonExistingUserId_ShouldReturnException(Account account, Role role)
        {
            await Db.InsertAsync(role);

            UserRole userRole = UserRoleData.GetMockUserRole(role.Id, account.UserId);
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(userRole));

            string foriegnKeyName = $"FK_{AliasConstants.UserRoles}_{AliasConstants.Accounts}_{nameof(UserRole.UserId)}";
            Assert.Equal(ExceptionConstants.ForeignKeyConstraintConflict(foriegnKeyName, TableNameConstants.Accounts, nameof(UserRole.UserId)), ex.Message);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task InsertAsync_NonExistingRoleId_ShouldReturnException(Account account, Role role)
        {
            await Db.InsertAsync(account);

            UserRole userRole = UserRoleData.GetMockUserRole(role.Id, account.UserId);
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(userRole));

            string foriegnKeyName = $"FK_{AliasConstants.UserRoles}_{AliasConstants.Roles}_{nameof(UserRole.RoleId)}";
            Assert.Equal(ExceptionConstants.ForeignKeyConstraintConflict(foriegnKeyName, TableNameConstants.Roles, nameof(Role.Id)), ex.Message);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task InsertAsync_VerifyNeverCalled(Account account, Role role)
        {
            await MockRepository.Object.InsertAsync(account.UserId, role.Name);
            MockRepository.Verify(r => r.InsertAsync(Guid.NewGuid(), role.Name), Times.Never);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task InsertAsync_VerifyCalledOnce(Account account, Role role)
        {
            await MockRepository.Object.InsertAsync(account.UserId, role.Name);
            MockRepository.Verify(r => r.InsertAsync(account.UserId, role.Name), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task InsertDuplicateUserRoles_ShouldReturnException(Account account, Role role)
        {
            UserRole userRole = await CreateUserRoleAndRelatedData(account, role);

            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(userRole));

            string uniqueConstraintName = $"'UC_{AliasConstants.UserRoles}_{nameof(UserRole.UserId)}_{nameof(UserRole.RoleId)}'";
            Assert.StartsWith($"{ExceptionConstants.ViolationOfUniqueKeyConstraint} {uniqueConstraintName}", ex.Message);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task GetUserRoles_ShouldReturnUserRole(Account account, Role role)
        {
            UserRole userRole = await CreateUserRoleAndRelatedData(account, role);

            IEnumerable<UserRole> actualUserRoles = await Repository.GetUserRoles(role.Name);
            AssertHelpers.AssertSingleEqual(userRole, actualUserRoles, nameof(UserRole.Id));
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserRoles_ShouldReturnEmpty(string roleName)
        {
            IEnumerable<UserRole> actualUserRoles = await Repository.GetUserRoles(roleName);
            Assert.Empty(actualUserRoles);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserRoles_VerifyNeverCalled(string roleName)
        {
            await MockRepository.Object.GetUserRoles(roleName);
            MockRepository.Verify(r => r.GetUserRoles(null), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserRoles_VerifyCalledOnce(string roleName)
        {
            await MockRepository.Object.GetUserRoles(roleName);
            MockRepository.Verify(r => r.GetUserRoles(roleName), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task DeleteUserRole_ShouldReturnTrue(Account account, Role role)
        {
            await CreateUserRoleAndRelatedData(account, role);

            bool isDeleted = await Repository.DeleteUserRole(account.UserId, role.Name);
            Assert.True(isDeleted);

            bool isInRole = await Repository.IsInRole(account.UserId, role.Name);
            Assert.False(isInRole);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedDataWithMultipleRoles), MemberType = typeof(UserRoleData))]
        public async Task DeleteUserRole_WithMultipleRoles_ShouldReturnTrue(Account account, IEnumerable<Role> roles)
        {
            await Db.InsertAsync(account);
            await Db.InsertAsync(roles);

            UserRole firstUserRole = UserRoleData.GetMockUserRole(roles.First().Id, account.UserId);
            UserRole secondUserRole = UserRoleData.GetMockUserRole(roles.Last().Id, account.UserId);

            await Db.InsertAsync(firstUserRole);
            await Db.InsertAsync(secondUserRole);

            bool isDeleted = await Repository.DeleteUserRole(account.UserId, roles.First().Name);
            Assert.True(isDeleted);

            bool isInRole = await Repository.IsInRole(account.UserId, roles.Last().Name);
            Assert.True(isInRole);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedDataWithMultipleAccounts), MemberType = typeof(UserRoleData))]
        public async Task DeleteUserRole_WithSomeUsers_ShouldReturnTrue(IEnumerable<Account> accounts, Role role)
        {
            await Db.InsertAsync(accounts);
            await Db.InsertAsync(role);

            UserRole firstUserRole = UserRoleData.GetMockUserRole(role.Id, accounts.First().UserId);
            UserRole secondUserRole = UserRoleData.GetMockUserRole(role.Id, accounts.Last().UserId);

            await Db.InsertAsync(firstUserRole);
            await Db.InsertAsync(secondUserRole);

            bool isDeleted = await Repository.DeleteUserRole(firstUserRole.UserId, role.Name);
            Assert.True(isDeleted);

            bool isInRole = await Repository.IsInRole(secondUserRole.UserId, role.Name);
            Assert.True(isInRole);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteAsync_ShouldReturnFalse(Guid userId, string roleName)
        {
            bool isDeleted = await Repository.DeleteUserRole(userId, roleName);
            Assert.False(isDeleted);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserRole_VerifyNeverCalled(Guid userId, string roleName)
        {
            await MockRepository.Object.DeleteUserRole(userId, roleName);
            MockRepository.Verify(r => r.DeleteUserRole(userId, null), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserRole_VerifyCalledOnce(Guid userId, string roleName)
        {
            await MockRepository.Object.DeleteUserRole(userId, roleName);
            MockRepository.Verify(r => r.DeleteUserRole(userId, roleName), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task IsInRole_ShouldReturnTrue(Account account, Role role)
        {
            await CreateUserRoleAndRelatedData(account, role);

            bool isInRole = await Repository.IsInRole(account.UserId, role.Name);
            Assert.True(isInRole);
        }

        [Theory]
        [InlineAutoData]
        public async Task IsInRole_EmptyUserRoles_ShouldReturnFalse(Guid userId, string roleName)
        {
            bool isInRole = await Repository.IsInRole(userId, roleName);
            Assert.False(isInRole);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task IsInRole_FilterByNonExistingUserId_ShouldReturnFalse(Account account, Role role)
        {
            await CreateUserRoleAndRelatedData(account, role);

            bool isInRole = await Repository.IsInRole(Guid.NewGuid(), role.Name);
            Assert.False(isInRole);
        }

        [Theory]
        [MemberData(nameof(UserRoleData.UserRoleRelatedData), MemberType = typeof(UserRoleData))]
        public async Task IsInRole_FilterByNonExistingRoleName_ShouldReturnFalse(Account account, Role role)
        {
            await CreateUserRoleAndRelatedData(account, role);

            bool isInRole = await Repository.IsInRole(account.UserId, RandomGeneratorHelpers.GenerateRandomValue());
            Assert.False(isInRole);
        }

        [Theory]
        [InlineAutoData]
        public async Task IsInRole_VerifyNeverCalled(Guid userId, string roleName)
        {
            await MockRepository.Object.IsInRole(userId, roleName);
            MockRepository.Verify(r => r.IsInRole(userId, null), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task IsInRole_VerifyCalledOnce(Guid userId, string roleName)
        {
            await MockRepository.Object.IsInRole(userId, roleName);
            MockRepository.Verify(r => r.IsInRole(userId, roleName), Times.Once);
        }

        #region Private Methods

        private async Task<UserRole> CreateUserRoleAndRelatedData(Account account, Role role)
        {
            await Db.InsertAsync(account);
            await Db.InsertAsync(role);

            UserRole userRole = UserRoleData.GetMockUserRole(role.Id, account.UserId);
            await Db.InsertAsync(userRole);

            return userRole;
        }

        #endregion Private Methods
    }
}
