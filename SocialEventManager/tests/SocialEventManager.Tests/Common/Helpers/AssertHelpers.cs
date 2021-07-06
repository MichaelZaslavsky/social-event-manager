using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SocialEventManager.Tests.Common.Helpers
{
    public static class AssertHelpers
    {
        public static void AssertObjectsEqual<T1, T2>(T1 expected, T2 actual, params string[] ignoreList) =>
            Assert.True(ObjectHelpers.AreObjectsEqual(actual, expected, ignoreList));

        public static void AssertObjectsEqual<T1, T2>(IEnumerable<T1> expected, IEnumerable<T2> actual, params string[] ignoreList) =>
            Assert.True(ObjectHelpers.AreObjectsEqual(actual.OrderBy(a => a), expected.OrderBy(e => e), ignoreList));

        public static void AssertSingleEqual<T1, T2>(T1 expected, IEnumerable<T2> actual, params string[] ignoreList)
        {
            Assert.NotEmpty(actual);
            Assert.Single(actual);
            AssertObjectsEqual(actual.Single(), expected, ignoreList);
        }
    }
}
