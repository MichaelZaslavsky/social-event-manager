using System.Collections.Generic;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class UserRoleData
    {
        public static IEnumerable<object[]> UserRoleRelatedData
        {
            get
            {
                yield return new object[] { AccountData.GetMockAccount(), RoleData.GetMockRole() };
            }
        }
    }
}
