using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.Helpers;

namespace SocialEventManager.Tests.DataMembers.Common;

public static class EnumerableData
{
    public static IEnumerable<object[]> EmptyData
    {
        get
        {
            yield return new object[] { Enumerable.Empty<int>(), true };
            yield return new object[] { TestRandomGeneratorHelpers.NextInt32s(), false };
        }
    }

    public static IEnumerable<object[]> NullOrEmptyData
    {
        get
        {
            yield return new object[] { null!, true };
            yield return new object[] { Enumerable.Empty<int>(), true };
            yield return new object[] { TestRandomGeneratorHelpers.NextInt32s(), false };
        }
    }

    public static IEnumerable<object[]> NotNullAndAnyData
    {
        get
        {
            yield return new object[] { TestRandomGeneratorHelpers.NextInt32s(), true };
            yield return new object[] { Enumerable.Empty<int>(), false };
            yield return new object[] { null!, false };
        }
    }

    public static IEnumerable<object[]> UpdateInForEachData
    {
        get
        {
            yield return new object[]
            {
                    new List<Role>
                    {
                        RoleData.GetMockRole(RoleType.User.GetDescription()),
                        RoleData.GetMockRole(RoleType.Admin.GetDescription()),
                    },
                    DataConstants.LoremIpsum,
            };
            yield return new object[]
            {
                    new List<Role>(),
                    DataConstants.LoremIpsum,
            };
        }
    }
}
