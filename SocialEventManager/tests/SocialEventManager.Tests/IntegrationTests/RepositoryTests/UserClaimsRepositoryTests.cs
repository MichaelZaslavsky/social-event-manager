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
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.RepositoryTests
{
    [Collection(DataConstants.RepositoryTests)]
    [IntegrationTest]
    [Category(CategoryConstants.Identity)]
    public class UserClaimsRepositoryTests : RepositoryTestBase<IUserClaimsRepository, UserClaim>
    {
        public UserClaimsRepositoryTests(IInMemoryDatabase db, IUserClaimsRepository userClaimsRepository)
            : base(db, userClaimsRepository)
        {
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task GetUserClaims_Should_Return_UserClaims(UserClaim userClaim)
        {
            userClaim.Id = await CreateUserClaimAndRelatedData(userClaim);

            IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(userClaim.Type, userClaim.Value);
            actualUserClaims.Should().ContainSingle(uc => uc.IsDeepEqual(userClaim));
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameUser), MemberType = typeof(UserClaimData))]
        public async Task GetUserClaims_Multiple_Should_Return_UserClaimPerUser(IEnumerable<UserClaim> userClaims)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaims.First().UserId));
            await Db.InsertAsync(userClaims);

            userClaims.ForEach(async userClaim =>
            {
                IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(userClaim.Type, userClaim.Value);
                actualUserClaims.Should().ContainSingle(actualUserClaim => actualUserClaim.IsDeepEqual(userClaim));
            });
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameType), MemberType = typeof(UserClaimData))]
        public async Task GetUserClaims_Multiple_Should_Return_UserClaimPerType(IEnumerable<UserClaim> userClaims)
        {
            foreach (UserClaim userClaim in userClaims)
            {
                await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            }

            await Db.InsertAsync(userClaims);

            userClaims.ForEach(async userClaim =>
            {
                IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(userClaim.Type, userClaim.Value);
                actualUserClaims.Should().ContainSingle(actualUserClaim => actualUserClaim.IsDeepEqual(userClaim));
            });
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameTypeAndValue), MemberType = typeof(UserClaimData))]

        public async Task GetUserClaims_Multiple_Should_Return_UserClaims(IEnumerable<UserClaim> userClaims)
        {
            foreach (UserClaim userClaim in userClaims)
            {
                await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            }

            await Db.InsertAsync(userClaims);

            UserClaim firstUserClaim = userClaims.First();
            IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(firstUserClaim.Type, firstUserClaim.Value);

            actualUserClaims.Should().BeEquivalentTo(userClaims);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserClaims_Should_Return_Empty_UserClaims(string type, string value)
        {
            IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(type, value);
            actualUserClaims.Should().BeEmpty();
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserClaims_Verify_NeverCalled(string type, string value)
        {
            await MockRepository.Object.GetUserClaims(type, value);
            MockRepository.Verify(r => r.GetUserClaims(null, value), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserClaims_Verify_CalledOnce(string type, string value)
        {
            await MockRepository.Object.GetUserClaims(type, value);
            MockRepository.Verify(r => r.GetUserClaims(type, value), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteUserClaim_Should_Return_True(UserClaim userClaim)
        {
            userClaim.Id = await CreateUserClaimAndRelatedData(userClaim);

            bool isDeleted = await Repository.DeleteUserClaims(new List<UserClaim> { userClaim });
            isDeleted.Should().BeTrue();

            UserClaim userClaimAfterDeletion = await Db.SingleWhereAsync<UserClaim>(nameof(UserClaim.Id), userClaim.Id);
            userClaimAfterDeletion.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameUser), MemberType = typeof(UserClaimData))]
        public async Task DeleteUserClaims_Should_Return_True(IEnumerable<UserClaim> userClaims)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaims.First().UserId));
            await Db.InsertAsync(userClaims);

            bool isDeleted = await Repository.DeleteUserClaims(userClaims);
            isDeleted.Should().BeTrue();

            IEnumerable<UserClaim> userClaimAfterDeletion = await Db.SelectAsync<UserClaim>();
            userClaimAfterDeletion.Should().BeEmpty();
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserClaims_Should_Return_False(IEnumerable<UserClaim> userClaims)
        {
            bool isDeleted = await Repository.DeleteUserClaims(userClaims);
            isDeleted.Should().BeFalse();
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserClaims_Verify_NeverCalled(IEnumerable<UserClaim> userClaims)
        {
            await MockRepository.Object.DeleteUserClaims(userClaims);
            MockRepository.Verify(r => r.DeleteUserClaims(null), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserClaims_Verify_CalledOnce(IEnumerable<UserClaim> userClaims)
        {
            await MockRepository.Object.DeleteUserClaims(userClaims);
            MockRepository.Verify(r => r.DeleteUserClaims(userClaims), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteAsync_Should_Return_True(UserClaim userClaim)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            int userClaimId = await Db.InsertAsync(userClaim);

            bool isDeleted = await Repository.DeleteAsync(userClaimId, nameof(UserClaim.Id));
            isDeleted.Should().BeTrue();

            IEnumerable<UserClaim> userClaimAfterDeletion = await Db.SelectAsync<UserClaim>();
            userClaimAfterDeletion.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameUserAndType), MemberType = typeof(UserClaimData))]
        public async Task InsertDuplicateUserAndTypeClaims_Should_Return_SqlException(IEnumerable<UserClaim> userClaims)
        {
            UserClaim firstUserClaim = userClaims.First();
            await Db.InsertAsync(AccountData.GetMockAccount(firstUserClaim.UserId));

            string uniqueConstraintName = $"UC_{AliasConstants.UserClaims}_{nameof(UserClaim.UserId)}_{nameof(UserClaim.Type)}";
            string duplicateKeyValue = $"{firstUserClaim.UserId}, {firstUserClaim.Type}";
            string expectedMessage = ExceptionConstants.ViolationOfUniqueKeyConstraint(uniqueConstraintName, TableNameConstants.UserClaims, duplicateKeyValue);

            Func<Task> func = async () => await Db.InsertAsync(userClaims);
            await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task InsertAsync_NonExistingUserId_Should_Return_SqlException(UserClaim userClaim)
        {
            string foriegnKeyName = $"FK_{AliasConstants.UserClaims}_{AliasConstants.Accounts}_{nameof(UserClaim.UserId)}";
            string expectedMessage = ExceptionConstants.ForeignKeyConstraintConflict(foriegnKeyName, TableNameConstants.Accounts, nameof(UserClaim.UserId));

            Func<Task> func = async () => await Db.InsertAsync(userClaim);
            await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteUser_Should_Delete_Related_UserClaim(UserClaim userClaim)
        {
            Account account = AccountData.GetMockAccount(userClaim.UserId);

            await Db.InsertAsync(account);
            int userClaimId = await Db.InsertAsync(userClaim);

            await Db.DeleteAsync(account);

            UserClaim userClaimAfterDeletion = await Db.SingleWhereAsync<UserClaim>(nameof(UserClaim.Id), userClaimId);
            userClaimAfterDeletion.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimWithValidLength), MemberType = typeof(UserClaimData))]
        public async Task InsertAsync_ValidData_Should_Succeed(UserClaim userClaim)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            await Db.InsertAsync(userClaim);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimWithMissingRequiredFields), MemberType = typeof(UserClaimData))]
        [MemberData(nameof(UserClaimData.UserClaimWithExceededLength), MemberType = typeof(UserClaimData))]
        public async Task InsertAsync_InvalidData_Should_Return_SqlException(UserClaim userClaim, string expectedMessage)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));

            Func<Task> func = async () => await Db.InsertAsync(userClaim);
            await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
        }

        #region Private Methods

        private async Task<int> CreateUserClaimAndRelatedData(UserClaim userClaim)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            return await Db.InsertAsync(userClaim);
        }

        #endregion Private Methods
    }
}
