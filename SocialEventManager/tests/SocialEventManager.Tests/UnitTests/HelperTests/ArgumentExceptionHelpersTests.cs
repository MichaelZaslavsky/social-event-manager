using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.Common.DataMembers;
using SocialEventManager.Tests.DataMembers.Common;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.HelperTests;

[UnitTest]
[Category(CategoryConstants.Helpers)]
public sealed class ArgumentExceptionHelpersTests
{
    [Theory]
    [AutoData]
    public void ThrowIfNullOrEmpty_Should_NotThrowArgumentException_When_ArgumentHasValue(IEnumerable<object> argument)
    {
        Exception? exception = Record.Exception(() => ArgumentExceptionHelpers.ThrowIfNullOrEmpty(argument));
        exception.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(EnumerableData.NullOrEmpty), MemberType = typeof(EnumerableData))]
    public void ThrowIfNullOrEmpty_Should_ThrowArgumentException_When_ArgumentIsNullOrEmpty(IEnumerable<object>? argument)
    {
        Action action = () => ArgumentExceptionHelpers.ThrowIfNullOrEmpty(argument);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage(TestConstants.ValueCannotBeNullOrEmpty(nameof(argument)));
    }

    [Theory]
    [InlineData(null, TestConstants.SomeText)]
    [InlineData(new object[] { }, TestConstants.SomeText)]
    public void ThrowIfNullOrEmpty_Should_ThrowArgumentException_When_ArgumentIsNullOrEmptyAndParamNameIsPassed(IEnumerable<object>? argument, string paramName)
    {
        Action action = () => ArgumentExceptionHelpers.ThrowIfNullOrEmpty(argument, paramName);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage(TestConstants.ValueCannotBeNullOrEmpty(paramName));
    }

    [Theory]
    [AutoData]
    public void ThrowIfNullOrWhiteSpace_Should_NotThrowArgumentException_When_ArgumentHasValue(string argument)
    {
        Exception? exception = Record.Exception(() => ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(argument));
        exception.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(StringData.NullOrWhiteSpaceData), MemberType = typeof(StringData))]
    public void ThrowIfNullOrWhiteSpace_Should_ThrowArgumentException_When_ArgumentIsNullOrEmpty(string? argument)
    {
        Action action = () => ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(argument);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage(TestConstants.ValueCannotBeNullOrWhiteSpace(nameof(argument)));
    }

    [Theory]
    [InlineData(null, TestConstants.SomeText)]
    [InlineData("", TestConstants.SomeText)]
    [InlineData(" ", TestConstants.SomeText)]
    public void ThrowIfNullOrWhiteSpace_Should_ThrowArgumentException_When_ArgumentIsNullOrEmptyAndParamNameIsPassed(string? argument, string paramName)
    {
        Action action = () => ArgumentExceptionHelpers.ThrowIfNullOrWhiteSpace(argument, paramName);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage(TestConstants.ValueCannotBeNullOrWhiteSpace(paramName));
    }
}
