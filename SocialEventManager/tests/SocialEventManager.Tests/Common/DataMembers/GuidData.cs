using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

public static class GuidData
{
    public static TheoryData<Guid, bool> NotDefaultGuidData =>
        new()
        {
            { Guid.Empty, false },
            { Guid.NewGuid(), true },
        };
}
