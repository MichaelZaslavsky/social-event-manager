using System.Collections.Generic;
using System.Linq;
using SocialEventManager.Tests.Common.Helpers;

namespace SocialEventManager.Tests.DataMembers.Common
{
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
                yield return new object[] { null, true };
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
                yield return new object[] { null, false };
            }
        }
    }
}
