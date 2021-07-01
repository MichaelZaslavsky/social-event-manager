using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SocialEventManager.Tests.Common.Helpers
{
    public static class AssertHelpers
    {
        public static void AssertSingleEqual<T1, T2>(T1 expected, IEnumerable<T2> actual, params string[] ignoreList)
        {
            Assert.NotEmpty(actual);
            Assert.Single(actual);
            Assert.True(ObjectHelpers.AreObjectsEqual(actual.Single(), expected, ignoreList));
        }
    }
}
