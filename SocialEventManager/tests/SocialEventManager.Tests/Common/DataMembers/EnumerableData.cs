using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.Common.Helpers;
using Xunit;

namespace SocialEventManager.Tests.DataMembers.Common;

public static class EnumerableData
{
    public static TheoryData<IEnumerable<int>, bool> EmptyData =>
        new()
        {
            { Enumerable.Empty<int>(), true },
            { TestRandomGeneratorHelpers.NextInt32s(), false },
        };

    public static TheoryData<IEnumerable<object>?> NullOrEmpty =>
        new()
        {
            { null },
            { Enumerable.Empty<object>() },
        };

    public static TheoryData<IEnumerable<int>?, bool> NullOrEmptyData =>
        new()
        {
            { null, true },
            { Enumerable.Empty<int>(), true },
            { TestRandomGeneratorHelpers.NextInt32s(), false },
        };

    public static TheoryData<IEnumerable<int>?, bool> NotNullAndAnyData =>
        new()
        {
            { TestRandomGeneratorHelpers.NextInt32s(), true },
            { Enumerable.Empty<int>(), false },
            { null, false },
        };

    public static TheoryData<List<Role>, string> UpdateInForEachData =>
        new()
        {
            {
                new()
                {
                    RoleData.GetMockRole(RoleType.User.GetDescription()),
                    RoleData.GetMockRole(RoleType.Admin.GetDescription()),
                },
                DataConstants.LoremIpsum
            },
            {
                new(),
                DataConstants.LoremIpsum
            },
        };
}
