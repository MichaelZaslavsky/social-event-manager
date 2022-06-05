namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class GuidData
    {
        public static IEnumerable<object[]> NotDefaultGuidData
        {
            get
            {
                return new List<object[]>
                {
                    new object[]
                    {
                        Guid.Empty,
                        false,
                    },
                    new object[]
                    {
                        Guid.NewGuid(),
                        true,
                    },
                };
            }
        }
    }
}
