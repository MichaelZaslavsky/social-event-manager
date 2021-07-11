using AutoFixture.Xunit2;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.EqualityComparers;
using SocialEventManager.Tests.Common.DataMembers;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.EqualityComparerTests
{
    public class UserClaimEqualityComparerTests
    {
        [Theory]
        [InlineAutoData]
        public void CompareUserClaims_ShouldReturnTrue(UserClaim userClaim)
        {
            var userClaimEqualityComparer = new UserClaimEqualityComparer();
            bool isEqual = userClaimEqualityComparer.Equals(userClaim, userClaim);
            Assert.True(isEqual);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithDifferentIds), MemberType = typeof(UserClaimData))]
        public void CompareUserClaims_DifferentId_ShouldReturnTrue(UserClaim userClaim, UserClaim otherUserClaim)
        {
            var userClaimEqualityComparer = new UserClaimEqualityComparer();
            bool isEqual = userClaimEqualityComparer.Equals(userClaim, otherUserClaim);
            Assert.True(isEqual);
        }

        [Theory]
        [InlineAutoData]
        public void CompareUserClaims_ShouldReturnFalse(UserClaim userClaim, UserClaim otherUserClaim)
        {
            var userClaimEqualityComparer = new UserClaimEqualityComparer();
            bool isEqual = userClaimEqualityComparer.Equals(userClaim, otherUserClaim);
            Assert.False(isEqual);
        }
    }
}
