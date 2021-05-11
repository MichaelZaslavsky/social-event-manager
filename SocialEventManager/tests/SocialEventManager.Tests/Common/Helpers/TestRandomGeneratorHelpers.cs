using System.Collections.Generic;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.Helpers
{
    public static class TestRandomGeneratorHelpers
    {
        public static IEnumerable<int> NextInt32s(int itemsCount = 3)
        {
            if (itemsCount <= 0)
            {
                yield return -1;
            }

            for (int i = 0; i < itemsCount; i++)
            {
                yield return RandomGeneratorHelpers.NextInt32();
            }
        }
    }
}
