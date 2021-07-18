using System;
using System.Collections.Generic;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.DataMembers.Common;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests
{
    [UnitTest]
    [Category(CategoryConstants.Extensions)]
    public class EnumerableExtensionsTests
    {
        [Theory]
        [MemberData(nameof(EnumerableData.EmptyData), MemberType = typeof(EnumerableData))]
        public void IsEmpty_Should_Return_Expected_Result(IEnumerable<int> enumerable, bool expectedResult)
        {
            bool actualResult = enumerable.IsEmpty();
            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null)]
        public void IsEmpty_Should_Return_ArgumentNullException<T>(IEnumerable<T> enumerable)
        {
            Action action = () => enumerable.IsEmpty();
            action.Should().Throw<ArgumentNullException>().WithMessage(ExceptionConstants.ValueCannotBeNull("source"));
        }

        [Theory]
        [MemberData(nameof(EnumerableData.NullOrEmptyData), MemberType = typeof(EnumerableData))]
        public void IsNullOrEmpty_Should_Return_Expected_Result(IEnumerable<int> enumerable, bool expectedResult)
        {
            bool actualResult = enumerable.IsNullOrEmpty();
            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(EnumerableData.NotNullAndAnyData), MemberType = typeof(EnumerableData))]
        public void IsNotNullAndAny_Should_Return_Expected_Result(IEnumerable<int> enumerable, bool expectedResult)
        {
            bool actualResult = enumerable.IsNotNullAndAny();
            actualResult.Should().Be(expectedResult);
        }
    }
}
