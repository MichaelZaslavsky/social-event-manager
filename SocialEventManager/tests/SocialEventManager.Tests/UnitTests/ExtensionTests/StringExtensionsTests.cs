using System;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.DataMembers.Common;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests
{
    public class StringExtensionsTests
    {
        [Theory]
        [MemberData(nameof(StringData.NullOrEmptyData), MemberType = typeof(StringData))]
        public void IsNullOrEmpty(string value, bool expectedResult)
        {
            bool actualResult = value.IsNullOrEmpty();
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(StringData.NullOrEmptyData), MemberType = typeof(StringData))]
        [MemberData(nameof(StringData.WhiteSpaceData), MemberType = typeof(StringData))]
        public void IsNullOrWhiteSpace(string value, bool expectedResult)
        {
            bool actualResult = value.IsNullOrWhiteSpace();
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(StringData.TakeAfterData), MemberType = typeof(StringData))]
        [MemberData(nameof(StringData.TakeAfterFirstData), MemberType = typeof(StringData))]
        public void TaskAfterFirst(string value, string fromStart, StringComparison comparisonType, string expectedResult)
        {
            string actualResult = value.TakeAfterFirst(fromStart, comparisonType);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(StringData.TakeAfterData), MemberType = typeof(StringData))]
        [MemberData(nameof(StringData.TakeAfterLastData), MemberType = typeof(StringData))]
        public void TaskAfterLast(string value, string fromStart, StringComparison comparisonType, string expectedResult)
        {
            string actualResult = value.TakeAfterLast(fromStart, comparisonType);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(StringData.TakeUntilData), MemberType = typeof(StringData))]
        [MemberData(nameof(StringData.TakeUntilFirstData), MemberType = typeof(StringData))]
        public void TaskUntilFirst(string value, string fromEnd, StringComparison comparisonType, string expectedResult)
        {
            string actualResult = value.TakeUntilFirst(fromEnd, comparisonType);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(StringData.TakeUntilData), MemberType = typeof(StringData))]
        [MemberData(nameof(StringData.TakeUntilLastData), MemberType = typeof(StringData))]
        public void TaskUntilLast(string value, string fromEnd, StringComparison comparisonType, string expectedResult)
        {
            string actualResult = value.TakeUntilLast(fromEnd, comparisonType);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
