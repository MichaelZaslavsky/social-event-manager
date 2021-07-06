using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using DeepEqual.Syntax;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;
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
    [Collection(nameof(RolesRepositoryTests))]
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
        public async Task InsertRole(Role role)
        {
            Guid roleId = await Repository.InsertRole(role);

            Role actualRole = await Db.SingleWhereAsync<Role>(nameof(role.Id), roleId);
            Assert.True(actualRole.IsDeepEqual(role));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertRole_VerifyNeverCalled(Role role)
        {
            await MockRepository.Object.InsertRole(role);
            MockRepository.Verify(r => r.InsertRole(null), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertRole_VerifyCalledOnce(Role role)
        {
            await MockRepository.Object.InsertRole(role);
            MockRepository.Verify(r => r.InsertRole(role), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertDuplicateRole_ShouldReturnException(Role role)
        {
            await Db.InsertAsync(role);
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(role));
            Assert.StartsWith(ExceptionConstants.ViolationOfPrimaryKeyConstraint($"PK__{AliasConstants.Roles}__"), ex.Message);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertDuplicateRoleId_ShouldReturnException(Role role)
        {
            await Db.InsertAsync(role);

            Role duplicatedIdRole = RoleData.GetMockRole(RandomGeneratorHelpers.GenerateRandomValue(), role.Id);
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(duplicatedIdRole));

            Assert.StartsWith(ExceptionConstants.ViolationOfPrimaryKeyConstraint($"PK__{AliasConstants.Roles}__"), ex.Message);
        }

        [Theory]
        [MemberData(nameof(RoleData.RolesWithSameName), MemberType = typeof(RoleData))]
        public async Task InsertDuplicateRoleName_ShouldReturnException(IEnumerable<Role> roles)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(roles));

            string uniqueConstraintName = $"UC_{AliasConstants.Roles}_{nameof(Role.Name)}";
            string duplicateKeyValue = roles.First().Name;

            Assert.Equal(ex.Message, ExceptionConstants.ViolationOfUniqueKeyConstraint(uniqueConstraintName, TableNameConstants.Roles, duplicateKeyValue));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetByUserIdAsync_ShouldReturnRole(Role role)
        {
            await Db.CreateTableIfNotExistsAsync<Account>();
            await Db.CreateTableIfNotExistsAsync<UserRole>();

            Account account = AccountData.GetMockAccount();
            UserRole userRole = UserRoleData.GetMockUserRole(role.Id, account.UserId);

            await Db.InsertAsync(account);
            await Db.InsertAsync(role);
            await Db.InsertAsync(userRole);

            IEnumerable<Role> actualRoles = await Repository.GetByUserIdAsync(userRole.UserId);
            AssertHelpers.AssertSingleEqual(role, actualRoles);
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldReturnEmpty()
        {
            await Db.CreateTableIfNotExistsAsync<Account>();
            await Db.CreateTableIfNotExistsAsync<UserRole>();

            IEnumerable<Role> actualRoles = await Repository.GetByUserIdAsync(Guid.NewGuid());
            Assert.Empty(actualRoles);
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldReturnSqlException()
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Repository.GetByUserIdAsync(Guid.NewGuid()));

            string tableName = SqlMapperUtilities.GetTableName<UserRole>();
            Assert.Equal($"Invalid object name '{tableName}'.", ex.Message);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetByUserIdAsync_VerifyNeverCalled(Guid userId)
        {
            await MockRepository.Object.GetByUserIdAsync(userId);
            MockRepository.Verify(r => r.GetByUserIdAsync(Guid.NewGuid()), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetByUserIdAsync_VerifyCalledOnce(Guid userId)
        {
            await MockRepository.Object.GetByUserIdAsync(userId);
            MockRepository.Verify(r => r.GetByUserIdAsync(userId), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.RoleWithValidLength), MemberType = typeof(RoleData))]
        public async Task InsertAsync_ValidData(Role role)
        {
            await Db.InsertAsync(role);
        }

        [Theory]
        [MemberData(nameof(RoleData.RoleWithMissingRequiredFields), MemberType = typeof(RoleData))]
        [MemberData(nameof(RoleData.RoleWithExceededLength), MemberType = typeof(RoleData))]
        public async Task InsertAsync_InvalidData(Role role, string expectedResult)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(role));
            Assert.Equal(expectedResult, ex.Message);
        }
    }
}
