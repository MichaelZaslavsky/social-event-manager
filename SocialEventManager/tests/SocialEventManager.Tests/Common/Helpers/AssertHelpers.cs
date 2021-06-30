using System.Collections.Generic;
using System.Linq;
using DeepEqual.Syntax;
using Xunit;

namespace SocialEventManager.Tests.Common.Helpers
{
    public static class AssertHelpers
    {
        public static void AssertSingleEqual<T1, T2>(T1 expected, IEnumerable<T2> actual)
        {
            Assert.NotEmpty(actual);
            Assert.Single(actual);
            Assert.True(actual.Single().IsDeepEqual(expected));
        }
    }
}
