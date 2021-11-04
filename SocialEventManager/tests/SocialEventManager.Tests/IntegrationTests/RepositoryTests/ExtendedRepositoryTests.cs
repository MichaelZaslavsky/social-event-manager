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
        public async Task GetAsync_ByColumnName_Should_Return_Roles(Role role)
        {
            await Db.InsertAsync(role);

            IEnumerable<Role> actualRoles = await Repository.GetAsync(role.Name, nameof(Role.Name));
            actualRoles.Should().ContainSingle(r => r.IsDeepEqual(role));
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Should_Return_Empty_Roles(Role role)
        {
            IEnumerable<Role> actualRoles = await Repository.GetAsync(role.Name, nameof(Role.Name));
            actualRoles.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByIncompatibleColumnName_Should_Throw_SqlException(Role role)
        {
            Func<Task> func = async () => await Repository.GetAsync(role.Name, nameof(Role.Id));
            await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Verify_NeverCalled(Role role)
        {
            await MockRepository.Object.GetAsync(role.Name, nameof(Role.Name));
            MockRepository.Verify(r => r.GetAsync($"Different {role.Name}", nameof(Role.Name)), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Verify_CalledOnce(Role role)
        {
            await MockRepository.Object.GetAsync(role.Name, nameof(Role.Name));
            MockRepository.Verify(r => r.GetAsync(role.Name, nameof(Role.Name)), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Multiple_Should_Return_Roles(IEnumerable<Role> roles)
        {
            await Db.InsertAsync(roles);

            IEnumerable<Role> actualRoles = await Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
            actualRoles.Should().BeEquivalentTo(roles);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Multiple_Should_Return_Empty_Roles(IEnumerable<Role> roles)
        {
            IEnumerable<Role> actualRoles = await Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
            actualRoles.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByIncompatibleColumnName_Multiple_Should_Throw_SqlException(IEnumerable<Role> roles)
        {
            Func<Task> func = async () => await Repository.GetAsync(roles.Select(role => role.Name), nameof(Role.Id));
            await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Multiple_Verify_NeverCalled(IEnumerable<Role> roles)
        {
            await MockRepository.Object.GetAsync(roles.Select(role => role.Name), nameof(Role.Name));
            MockRepository.Verify(r => r.GetAsync($"Different {roles.Select(role => role.Name)}", nameof(Role.Name)), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRoles), MemberType = typeof(RoleData))]
        public async Task GetAsync_ByColumnName_Multiple_Verify_CalledOnce(IEnumerable<Role> roles)
        {
            IEnumerable<string> names = roles.Select(role => role.Name);

            await MockRepository.Object.GetAsync(names, nameof(Role.Name));
            MockRepository.Verify(r => r.GetAsync(names, nameof(Role.Name)), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByColumnName_Should_Return_Role(Role role)
        {
            await Db.InsertAsync(role);

            Role actualRole = await Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
            actualRole.Should().BeEquivalentTo(role);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByColumnName_Should_Return_Null(Role role)
        {
            Role actualRole = await Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
            actualRole.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByIncompatibleColumnName_Should_Throw_SqlException(Role role)
        {
            Func<Task> func = async () => await Repository.GetSingleOrDefaultAsync(role.Name, nameof(Role.Id));
            await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.ConversionFailedFromStringToUniqueIdentifier);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByColumnName_Verify_NeverCalled(Role role)
        {
            await MockRepository.Object.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
            MockRepository.Verify(r => r.GetSingleOrDefaultAsync($"Different {role.Name}", nameof(Role.Name)), Times.Never);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task GetSingleOrDefaultAsync_ByColumnName_Verify_CalledOnce(Role role)
        {
            await MockRepository.Object.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name));
            MockRepository.Verify(r => r.GetSingleOrDefaultAsync(role.Name, nameof(Role.Name)), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RoleData.ValidRole), MemberType = typeof(RoleData))]
        public async Task DeleteAsync_ByColumnName_Should_Return_True(Role role)
        {
            await Db.InsertAsync(role);

            bool isDeleted = await Repository.DeleteAsync(role.Id, nameof(Role.Id));
            isDeleted.Should().BeTrue();

            Role actualRole = await Db.SingleWhereAsync<Role>(nameof(Role.Id), role.Id);
            actualRole.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ByColumnName_Should_Return_False()
        {
            bool isDeleted = await Repository.DeleteAsync(Guid.NewGuid(), nameof(Role.Id));
            isDeleted.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_ByColumnName_Should_Throw_SqlException()
        {
            Func<Task> func = async () => await Repository.DeleteAsync(RandomGeneratorHelpers.NextInt32(), nameof(Role.Id));
            await func.Should().ThrowAsync<SqlException>().WithMessage(ExceptionConstants.UniqueIdentifierIsIncompatibleWithInt);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteAsync_ByColumnName_Verify_NeverCalled(Guid roleId)
        {
            await MockRepository.Object.DeleteAsync(roleId, nameof(Role.Id));
            MockRepository.Verify(r => r.DeleteAsync(Guid.NewGuid(), nameof(Role.Id)), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteAsync_ByColumnName_Verify_CalledOnce(Guid roleId)
        {
            await MockRepository.Object.DeleteAsync(roleId, nameof(Role.Id));
            MockRepository.Verify(r => r.DeleteAsync(roleId, nameof(Role.Id)), Times.Once);
        }
    }
}
