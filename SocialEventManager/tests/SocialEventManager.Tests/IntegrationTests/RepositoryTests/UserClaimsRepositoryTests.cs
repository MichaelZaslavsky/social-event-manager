using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.Helpers;
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
        public async Task GetUserClaims_ShouldReturnUserClaim(UserClaim userClaim)
        {
            userClaim.Id = await CreateUserClaimAndRelatedData(userClaim);

            IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(userClaim.Type, userClaim.Value);
            AssertHelpers.AssertSingleEqual(userClaim, actualUserClaims);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameUser), MemberType = typeof(UserClaimData))]
        public async Task GetUserClaims_ShouldReturnUserClaimPerUser(IEnumerable<UserClaim> userClaims)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaims.First().UserId));
            await Db.InsertAsync(userClaims);

            Assert.All(userClaims, async uc =>
            {
                IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(uc.Type, uc.Value);
                AssertHelpers.AssertSingleEqual(uc, actualUserClaims);
            });
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameType), MemberType = typeof(UserClaimData))]
        public async Task GetUserClaims_ShouldReturnUserClaimPerType(IEnumerable<UserClaim> userClaims)
        {
            foreach (UserClaim userClaim in userClaims)
            {
                await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            }

            await Db.InsertAsync(userClaims);

            Assert.All(userClaims, async uc =>
            {
                IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(uc.Type, uc.Value);
                AssertHelpers.AssertSingleEqual(uc, actualUserClaims);
            });
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameTypeAndValue), MemberType = typeof(UserClaimData))]

        public async Task GetUserClaims_ShouldReturnUserClaims(IEnumerable<UserClaim> userClaims)
        {
            foreach (UserClaim userClaim in userClaims)
            {
                await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            }

            await Db.InsertAsync(userClaims);

            UserClaim firstUserClaim = userClaims.First();
            IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(firstUserClaim.Type, firstUserClaim.Value);

            AssertHelpers.AssertObjectsEqual(userClaims, actualUserClaims);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserClaims_ShouldReturnEmpty(string type, string value)
        {
            IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(type, value);
            Assert.Empty(actualUserClaims);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserClaims_VerifyNeverCalled(string type, string value)
        {
            await MockRepository.Object.GetUserClaims(type, value);
            MockRepository.Verify(r => r.GetUserClaims(null, value), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserClaims_VerifyCalledOnce(string type, string value)
        {
            await MockRepository.Object.GetUserClaims(type, value);
            MockRepository.Verify(r => r.GetUserClaims(type, value), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteUserClaim_ShouldReturnTrue(UserClaim userClaim)
        {
            userClaim.Id = await CreateUserClaimAndRelatedData(userClaim);

            bool isDeleted = await Repository.DeleteUserClaims(new List<UserClaim> { userClaim });
            Assert.True(isDeleted);

            UserClaim userClaimAfterDeletion = await Db.SingleWhereAsync<UserClaim>(nameof(UserClaim.Id), userClaim.Id);
            Assert.Null(userClaimAfterDeletion);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameUser), MemberType = typeof(UserClaimData))]
        public async Task DeleteUserClaims_ShouldReturnTrue(IEnumerable<UserClaim> userClaims)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaims.First().UserId));
            await Db.InsertAsync(userClaims);

            bool isDeleted = await Repository.DeleteUserClaims(userClaims);
            Assert.True(isDeleted);

            IEnumerable<UserClaim> userClaimAfterDeletion = await Db.SelectAsync<UserClaim>();
            Assert.Empty(userClaimAfterDeletion);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserClaims_ShouldReturnFalse(IEnumerable<UserClaim> userClaims)
        {
            bool isDeleted = await Repository.DeleteUserClaims(userClaims);
            Assert.False(isDeleted);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserClaims_VerifyNeverCalled(IEnumerable<UserClaim> userClaims)
        {
            await MockRepository.Object.DeleteUserClaims(userClaims);
            MockRepository.Verify(r => r.DeleteUserClaims(null), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserClaims_VerifyCalledOnce(IEnumerable<UserClaim> userClaims)
        {
            await MockRepository.Object.DeleteUserClaims(userClaims);
            MockRepository.Verify(r => r.DeleteUserClaims(userClaims), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteAsync_ShouldReturnTrue(UserClaim userClaim)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            int userClaimId = await Db.InsertAsync(userClaim);

            bool isDeleted = await Repository.DeleteAsync(userClaimId, nameof(UserClaim.Id));
            Assert.True(isDeleted);

            IEnumerable<UserClaim> userClaimAfterDeletion = await Db.SelectAsync<UserClaim>();
            Assert.Empty(userClaimAfterDeletion);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameUserAndType), MemberType = typeof(UserClaimData))]
        public async Task InsertDuplicateUserAndTypeClaims_ShouldReturnException(IEnumerable<UserClaim> userClaims)
        {
            UserClaim firstUserClaim = userClaims.First();
            await Db.InsertAsync(AccountData.GetMockAccount(firstUserClaim.UserId));
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(userClaims));

            string uniqueConstraintName = $"UC_{AliasConstants.UserClaims}_{nameof(UserClaim.UserId)}_{nameof(UserClaim.Type)}";
            string duplicateKeyValue = $"{firstUserClaim.UserId}, {firstUserClaim.Type}";
            Assert.Equal(ExceptionConstants.ViolationOfUniqueKeyConstraint(uniqueConstraintName, TableNameConstants.UserClaims, duplicateKeyValue), ex.Message);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task InsertAsync_NonExistingUserId_ShouldReturnException(UserClaim userClaim)
        {
            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(userClaim));

            string foriegnKeyName = $"FK_{AliasConstants.UserClaims}_{AliasConstants.Accounts}_{nameof(UserClaim.UserId)}";
            Assert.Equal(ExceptionConstants.ForeignKeyConstraintConflict(foriegnKeyName, TableNameConstants.Accounts, nameof(UserClaim.UserId)), ex.Message);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteUser_ShouldDeleteRelatedUserClaim(UserClaim userClaim)
        {
            Account account = AccountData.GetMockAccount(userClaim.UserId);

            await Db.InsertAsync(account);
            int userClaimId = await Db.InsertAsync(userClaim);

            await Db.DeleteAsync(account);

            UserClaim userClaimAfterDeletion = await Db.SingleWhereAsync<UserClaim>(nameof(UserClaim.Id), userClaimId);
            Assert.Null(userClaimAfterDeletion);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimWithValidLength), MemberType = typeof(UserClaimData))]
        public async Task InsertAsync_ValidData(UserClaim userClaim)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            await Db.InsertAsync(userClaim);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimWithMissingRequiredFields), MemberType = typeof(UserClaimData))]
        [MemberData(nameof(UserClaimData.UserClaimWithExceededLength), MemberType = typeof(UserClaimData))]
        public async Task InsertAsync_InvalidData(UserClaim userClaim, string expectedResult)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));

            SqlException ex = await Assert.ThrowsAsync<SqlException>(() => Db.InsertAsync(userClaim));
            Assert.Equal(expectedResult, ex.Message);
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
