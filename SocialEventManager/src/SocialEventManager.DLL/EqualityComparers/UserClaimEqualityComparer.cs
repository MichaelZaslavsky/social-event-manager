using System.Diagnostics.CodeAnalysis;
using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.DAL.EqualityComparers
{
    public class UserClaimEqualityComparer : IEqualityComparer<UserClaim>
    {
        public bool Equals(UserClaim userClaim, UserClaim otherUserClaim)
        {
            if (userClaim == null && otherUserClaim == null)
            {
                return true;
            }

            if (userClaim == null || otherUserClaim == null)
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
}
