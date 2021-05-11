using System;
using System.Collections.Generic;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.DataMembers.Common;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests
{
    public class EnumerableExtensionsTests
    {
        [Theory]
        [MemberData(nameof(EnumerableData.EmptyData), MemberType = typeof(EnumerableData))]
        public void IsEmpty<T>(IEnumerable<T> enumerable, bool expectedResult)
        {
            bool actualResult = enumerable.IsEmpty();
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(null)]
        public void IsEmpty_Expect_ArgumentNullException<T>(IEnumerable<T> enumerable)
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => enumerable.IsEmpty());
            Assert.Equal(ExceptionConstants.ValueCannotBeNull, exception.Message);
        }

        [Theory]
        [MemberData(nameof(EnumerableData.NullOrEmptyData), MemberType = typeof(EnumerableData))]
        public void IsNullOrEmpty<T>(IEnumerable<T> enumerable, bool expectedResult)
        {
            bool actualResult = enumerable.IsNullOrEmpty();
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(EnumerableData.NotNullAndAnyData), MemberType = typeof(EnumerableData))]
        public void IsNotNullAndAny<T>(IEnumerable<T> enumerable, bool expectedResult)
        {
            bool actualResult = enumerable.IsNotNullAndAny();
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
