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
        public async Task GetUserClaims_Should_ReturnUserClaims_When_FilteringByExistingTypeAndValue(UserClaim userClaim)
        {
            userClaim.Id = await CreateUserClaimAndRelatedData(userClaim);

            IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(userClaim.Type, userClaim.Value);
            actualUserClaims.Should().ContainSingle(uc => uc.IsDeepEqual(userClaim));
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameUser), MemberType = typeof(UserClaimData))]
        public async Task GetUserClaims_Should_ReturnUserClaimPerUser_When_FilteringByExistingTypeAndValue(IEnumerable<UserClaim> userClaims)
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
        public async Task GetUserClaims_Should_ReturnUserClaimPerType_When_FilteringByExistingTypeAndValue(IEnumerable<UserClaim> userClaims)
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

        public async Task GetUserClaims_Should_ReturnUserClaims_When_FilteringByExistingTypeAndValueOfTheFirstUser(IEnumerable<UserClaim> userClaims)
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
        public async Task GetUserClaims_Should_ReturnEmptyUserClaims_When_FilteringByNonExistingValueOrType(string type, string value)
        {
            IEnumerable<UserClaim> actualUserClaims = await Repository.GetUserClaims(type, value);
            actualUserClaims.Should().BeEmpty();
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserClaims_Should_VerifyNeverCalled_When_TypeHasDifferentValue(string type, string value)
        {
            await MockRepository.Object.GetUserClaims(type, value);
            MockRepository.Verify(r => r.GetUserClaims(null, value), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task GetUserClaims_Should_VerifyCalledOnce_When_TypeAndValueValuesAreRepetead(string type, string value)
        {
            await MockRepository.Object.GetUserClaims(type, value);
            MockRepository.Verify(r => r.GetUserClaims(type, value), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteUserClaims_Should_ReturnTrue_When_UserClaimExists(UserClaim userClaim)
        {
            userClaim.Id = await CreateUserClaimAndRelatedData(userClaim);

            bool isDeleted = await Repository.DeleteUserClaims(new List<UserClaim> { userClaim });
            isDeleted.Should().BeTrue();

            UserClaim userClaimAfterDeletion = await Db.SingleWhereAsync<UserClaim>(nameof(UserClaim.Id), userClaim.Id);
            userClaimAfterDeletion.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithSameUser), MemberType = typeof(UserClaimData))]
        public async Task DeleteUserClaims_Should_ReturnTrue_When_UserClaimsExist(IEnumerable<UserClaim> userClaims)
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
        public async Task DeleteUserClaims_Should_ReturnFalse_When_UserClaimsNotExists(IEnumerable<UserClaim> userClaims)
        {
            bool isDeleted = await Repository.DeleteUserClaims(userClaims);
            isDeleted.Should().BeFalse();
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserClaims_Should_VerifyNeverCalled_When_UserClaimsHaveDifferentValue(IEnumerable<UserClaim> userClaims)
        {
            await MockRepository.Object.DeleteUserClaims(userClaims);
            MockRepository.Verify(r => r.DeleteUserClaims(null), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task DeleteUserClaims_Should_VerifyCalledOnce_When_UserClaimsHaveSameValue(IEnumerable<UserClaim> userClaims)
        {
            await MockRepository.Object.DeleteUserClaims(userClaims);
            MockRepository.Verify(r => r.DeleteUserClaims(userClaims), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteAsync_Should_ReturnTrue_When_UserClaimExists(UserClaim userClaim)
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
        public async Task InsertAsync_Should_ThrowSqlException_When_UserTypeAndClaimsAreDuplicated(IEnumerable<UserClaim> userClaims)
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
        public async Task InsertAsync_Should_ThrowSqlException_When_UserIdNotExists(UserClaim userClaim)
        {
            string foriegnKeyName = $"FK_{AliasConstants.UserClaims}_{AliasConstants.Accounts}_{nameof(UserClaim.UserId)}";
            string expectedMessage = ExceptionConstants.ForeignKeyConstraintConflict(foriegnKeyName, TableNameConstants.Accounts, nameof(UserClaim.UserId));

            Func<Task> func = async () => await Db.InsertAsync(userClaim);
            await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.ValidUserClaim), MemberType = typeof(UserClaimData))]
        public async Task DeleteUser_Should_DeleteRelatedUserClaim_When_UserHasARelatedUserClaim(UserClaim userClaim)
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
        public async Task InsertAsync_Should_Succeed_When_UserClaimIsValid(UserClaim userClaim)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));
            await Db.InsertAsync(userClaim);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimWithMissingRequiredFields), MemberType = typeof(UserClaimData))]
        public async Task InsertAsync_Should_ThrowSqlException_When_RequiredFieldsAreMissing(UserClaim userClaim, string expectedMessage)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));

            Func<Task> func = async () => await Db.InsertAsync(userClaim);
            await func.Should().ThrowAsync<SqlException>().WithMessage(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimWithExceededLength), MemberType = typeof(UserClaimData))]
        public async Task InsertAsync_Should_ThrowSqlException_When_LengthIsExceeded(UserClaim userClaim, string expectedMessage)
        {
            await Db.InsertAsync(AccountData.GetMockAccount(userClaim.UserId));

            Func<Task> func = async () => await Db.InsertAsync(userClaim);
            (await func.Should().ThrowAsync<SqlException>()).And.Message.Should().StartWith(expectedMessage);
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
