using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.DataMembers.Common;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.HelperTests;

[UnitTest]
[Category(CategoryConstants.Helpers)]
public class ArgumentExceptionHelpersTests
{
    [Theory]
    [InlineAutoData]
    public void ThrowIfNullOrEmpty_Should_NotThrowArgumentException_When_ArgumentHasValue(IEnumerable<object> argument)
    {
        Exception? exception = Record.Exception(() => ArgumentExceptionHelpers.ThrowIfNullOrEmpty(argument, nameof(argument)));
        exception.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(EnumerableData.NullOrEmpty), MemberType = typeof(EnumerableData))]
    public void ThrowIfNullOrEmpty_Should_ThrowArgumentException_When_ArgumentIsNullOrEmpty(IEnumerable<object>? argument)
    {
        Action action = () => ArgumentExceptionHelpers.ThrowIfNullOrEmpty(argument, nameof(argument));

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage(TestConstants.ValueCannotBeNullOrEmpty(nameof(argument)));
    }

    [Theory]
    [InlineAutoData]
    public void ThrowIfNullOrWhiteSpace_Should_NotThrowArgumentException_When_ArgumentHasValue(string argument)
    {
        Exception? exception = Record.Exception(() => ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(argument, nameof(argument)));
        exception.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(StringData.NullOrWhiteSpaceData), MemberType = typeof(StringData))]
    public void ThrowIfNullOrWhiteSpace_Should_ThrowArgumentException_When_ArgumentIsNullOrEmpty(string? argument)
    {
        Action action = () => ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(argument, nameof(argument));

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage(TestConstants.ValueCannotBeNullOrWhiteSpace(nameof(argument)));
    }
}
