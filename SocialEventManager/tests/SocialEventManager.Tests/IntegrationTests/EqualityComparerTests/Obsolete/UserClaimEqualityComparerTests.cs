// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.EqualityComparers;
using SocialEventManager.Tests.Common.DataMembers;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.EqualityComparerTests;

public class UserClaimEqualityComparerTests
{
    [Theory]
    [AutoData]
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
    [AutoData]
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
    [AutoData]
    public void CompareUserClaims_GetHashCode_Should_ReturnDifferentHash_When_UserClaimsAreDifferent(UserClaim userClaim, UserClaim otherUserClaim)
    {
        UserClaimEqualityComparer comparer = new();
        comparer.GetHashCode(userClaim).Should().NotBe(comparer.GetHashCode(otherUserClaim));
    }
}
*/
