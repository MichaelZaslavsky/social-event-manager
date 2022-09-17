// This is an example of a partial Identity implementation with Dapper.
// It was just for learning purposes.
// It is much more recommended to use the Identity packages with EF and not reinventing the wheel.

/*
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Shared.EqualityComparers;

public sealed class UserClaimEqualityComparer : IEqualityComparer<UserClaim>
{
    public bool Equals(UserClaim? userClaim, UserClaim? otherUserClaim)
    {
        if (userClaim is null && otherUserClaim is null)
        {
            return true;
        }

        if (userClaim is null || otherUserClaim is null)
        {
            return false;
        }

        return userClaim.UserId == otherUserClaim.UserId
            && userClaim.Type == otherUserClaim.Type
            && userClaim.Value == otherUserClaim.Value;
    }

    public int GetHashCode([DisallowNull] UserClaim obj) =>
        HashingHelpers.RSHash(obj.UserId, obj.Type, obj.Value);
}
*/
