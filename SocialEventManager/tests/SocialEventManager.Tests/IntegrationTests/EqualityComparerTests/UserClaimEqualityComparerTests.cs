using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.EqualityComparers;
using SocialEventManager.Tests.Common.DataMembers;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.EqualityComparerTests;

public class UserClaimEqualityComparerTests
{
    [Theory]
    [InlineAutoData]
    public void CompareUserClaims_Should_ReturnTrue_When_UserClaimsAreTheSame(UserClaim userClaim)
    {
        UserClaimEqualityComparer comparer = new();
        bool isEqual = comparer.Equals(userClaim, userClaim);
        isEqual.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(UserClaimData.UserClaimsWithDifferentIds), MemberType = typeof(UserClaimData))]
    public void CompareUserClaims_Should_ReturnTrue_When_UserClaimsHaveDifferentId(UserClaim userClaim, UserClaim otherUserClaim)
    {
        UserClaimEqualityComparer comparer = new();
        bool isEqual = comparer.Equals(userClaim, otherUserClaim);
        isEqual.Should().BeTrue();
    }

    [Fact]
    public void CompareUserClaims_Should_ReturnTrue_When_UserClaimsAreNulls()
    {
        UserClaimEqualityComparer comparer = new();
        bool isEqual = comparer.Equals(null, null);
        isEqual.Should().BeTrue();
    }

    [Theory]
    [InlineAutoData]
    [MemberData(nameof(UserClaimData.UserClaimsWithOneNull), MemberType = typeof(UserClaimData))]
    public void CompareUserClaims_Should_ReturnFalse_When_OneOfTheUserClaimsIsNull(UserClaim? userClaim, UserClaim? otherUserClaim)
    {
        UserClaimEqualityComparer comparer = new();
        bool isEqual = comparer.Equals(userClaim, otherUserClaim);
        isEqual.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(UserClaimData.UserClaimsWithDifferentIds), MemberType = typeof(UserClaimData))]
    public void CompareUserClaims_GetHashCode_Should_ReturnSameHash_When_UserClaimsHaveDifferentId(UserClaim userClaim, UserClaim otherUserClaim)
    {
        UserClaimEqualityComparer comparer = new();
        comparer.GetHashCode(userClaim).Should().Be(comparer.GetHashCode(otherUserClaim));
    }

    [Theory]
    [InlineAutoData]
    public void CompareUserClaims_GetHashCode_Should_ReturnDifferentHash_When_UserClaimsAreDifferent(UserClaim userClaim, UserClaim otherUserClaim)
    {
        UserClaimEqualityComparer comparer = new();
        comparer.GetHashCode(userClaim).Should().NotBe(comparer.GetHashCode(otherUserClaim));
    }
}
