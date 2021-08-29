using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests
{
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
        public async Task InsertRole_Should_Return_RoleId(Role role)
        {
            Guid roleId = await Repository.InsertRole(role);

            Role actualRole = await Db.SingleWhereAsync<Role>(nameof(role.Id), roleId);
            actualRole.Should().BeEquivalentTo(role);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertRole_Verify_NeverCalled(Role role)
        {
            await MockRepository.Object.InsertRole(role);
            MockRepository.Verify(r => r.InsertRole(null), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertRole_Verify_CalledOnce(Role role)
        {
            await MockRepository.Object.InsertRole(role);
            MockRepository.Verify(r => r.InsertRole(role), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertDuplicateRole_Should_Return_SqlException(Role role)
        {
            await Db.InsertAsync(role);

            Func<Task> func = async () => await Db.InsertAsync(role);
            string message = (await func.Should().ThrowAsync<SqlException>()).Subject.First().Message;
            message.Should().StartWith(ExceptionConstants.ViolationOfPrimaryKeyConstraint($"PK__{AliasConstants.Roles}__"));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task InsertDuplicateRoleId_Should_Return_SqlException(Role role)
        {
            await Db.InsertAsync(role);

            Role duplicatedIdRole = RoleData.GetMockRole(RandomGeneratorHelpers.GenerateRandomValue(), role.Id);

            Func<Task> func = async () => await Db.InsertAsync(duplicatedIdRole);
            string message = (await func.Should().ThrowAsync<SqlException>()).Subject.First().Message;
            message.Should().StartWith(ExceptionConstants.ViolationOfPrimaryKeyConstraint($"PK__{AliasConstants.Roles}__"));
        }

        [Theory]
        [MemberData(nameof(RoleData.RolesWithSameName), MemberType = typeof(RoleData))]
        public async Task InsertDuplicateRoleName_Should_Return_SqlException(IEnumerable<Role> roles)
        {
            string uniqueConstraintName = $"UC_{AliasConstants.Roles}_{nameof(Role.Name)}";
            string duplicateKeyValue = roles.First().Name;

            Func<Task> func = async () => await Db.InsertAsync(roles);
            await func.Should().ThrowAsync<SqlException>()
                .WithMessage(ExceptionConstants.ViolationOfUniqueKeyConstraint(uniqueConstraintName, TableNameConstants.Roles, duplicateKeyValue));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetByUserIdAsync_Should_Return_Roles(Role role)
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
        public async Task GetByUserIdAsync_Should_Return_Empty_Roles()
        {
            await Db.CreateTableIfNotExistsAsync<Account>();
            await Db.CreateTableIfNotExistsAsync<UserRole>();

            IEnumerable<Role> actualRoles = await Repository.GetByUserIdAsync(Guid.NewGuid());
            actualRoles.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByUserIdAsync_Should_Return_SqlException()
        {
            string tableName = SqlMapperUtilities.GetTableName<UserRole>();

            Func<Task> func = async () => await Repository.GetByUserIdAsync(Guid.NewGuid());
            await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.InvalidObjectName(tableName));
        }

        [Theory]
        [InlineAutoData]
        public async Task GetByUserIdAsync_Verify_NeverCalled(Guid userId)
        {
            await MockRepository.Object.GetByUserIdAsync(userId);
            MockRepository.Verify(r => r.GetByUserIdAsync(Guid.NewGuid()), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetByUserIdAsync_Verify_CalledOnce(Guid userId)
        {
            await MockRepository.Object.GetByUserIdAsync(userId);
            MockRepository.Verify(r => r.GetByUserIdAsync(userId), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.RoleWithValidLength), MemberType = typeof(RoleData))]
        public async Task InsertAsync_ValidData_Should_Succeed(Role role)
        {
            await Db.InsertAsync(role);
        }

        [Theory]
        [MemberData(nameof(RoleData.RoleWithMissingRequiredFields), MemberType = typeof(RoleData))]
        [MemberData(nameof(RoleData.RoleWithExceededLength), MemberType = typeof(RoleData))]
        public async Task InsertAsync_InvalidData_Should_Return_SqlException(Role role, string expectedMessage)
        {
            Func<Task> func = async () => await Db.InsertAsync(role);
            await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
        }
    }
}
