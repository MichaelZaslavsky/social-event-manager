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
            var comparer = new UserClaimEqualityComparer();
            bool isEqual = comparer.Equals(userClaim, userClaim);
            Assert.True(isEqual);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithDifferentIds), MemberType = typeof(UserClaimData))]
        public void CompareUserClaims_DifferentId_ShouldReturnTrue(UserClaim userClaim, UserClaim otherUserClaim)
        {
            var comparer = new UserClaimEqualityComparer();
            bool isEqual = comparer.Equals(userClaim, otherUserClaim);
            Assert.True(isEqual);
        }

        [Fact]
        public void CompareUserClaims_BothNulls_ShouldReturnTrue()
        {
            var comparer = new UserClaimEqualityComparer();
            bool isEqual = comparer.Equals(null, null);
            Assert.True(isEqual);
        }

        [Theory]
        [InlineAutoData]
        [MemberData(nameof(UserClaimData.UserClaimsWithOneNull), MemberType = typeof(UserClaimData))]
        public void CompareUserClaims_ShouldReturnFalse(UserClaim userClaim, UserClaim otherUserClaim)
        {
            var comparer = new UserClaimEqualityComparer();
            bool isEqual = comparer.Equals(userClaim, otherUserClaim);
            Assert.False(isEqual);
        }

        [Theory]
        [MemberData(nameof(UserClaimData.UserClaimsWithDifferentIds), MemberType = typeof(UserClaimData))]
        public void CompareUserClaims_GetHashCode_DifferentId_ShouldReturnSameHash(UserClaim userClaim, UserClaim otherUserClaim)
        {
            var comparer = new UserClaimEqualityComparer();
            Assert.Equal(comparer.GetHashCode(userClaim), comparer.GetHashCode(otherUserClaim));
        }

        [Theory]
        [InlineAutoData]
        public void CompareUserClaims_GetHashCode_ShouldReturnDifferentHash(UserClaim userClaim, UserClaim otherUserClaim)
        {
            var comparer = new UserClaimEqualityComparer();
            Assert.NotEqual(comparer.GetHashCode(userClaim), comparer.GetHashCode(otherUserClaim));
        }
    }
}
