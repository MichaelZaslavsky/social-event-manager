using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Shared.EqualityComparers;

public class UserClaimEqualityComparer : IEqualityComparer<UserClaim>
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
