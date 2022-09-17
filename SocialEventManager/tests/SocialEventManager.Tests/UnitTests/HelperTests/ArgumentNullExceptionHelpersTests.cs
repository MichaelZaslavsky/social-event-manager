using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.HelperTests;

public sealed class ArgumentNullExceptionHelpersTests
{
    [Theory]
    [AutoData]
    public void ThrowIfNull_Should_ReturnOk_When_AllParametersAreNotNull(string argument1, int argument2, bool argument3)
    {
        Exception? exception = Record.Exception(() =>
            ArgumentNullExceptionHelpers.ThrowIfNull((argument1, nameof(argument1)), (argument2, nameof(argument2)), (argument3, nameof(argument3))));

        exception.Should().BeNull();
    }

    [Theory]
    [InlineData(null, 0, false, "argument1")]
    [InlineData(null, 0, null, "argument1")]
    [InlineData(null, null, false, "argument1")]
    [InlineData(null, null, null, "argument1")]
    [InlineData(DataConstants.RandomText, null, false, "argument2")]
    [InlineData(DataConstants.RandomText, null, null, "argument2")]
    [InlineData(DataConstants.RandomText, 0, null, "argument3")]
    public void ThrowIfNull_Should_ThrowArgumentNullException_When_AtLeastOneParameterIsNull(string argument1, int? argument2, bool? argument3, string expectedParameterName)
    {
        Action action = () => ArgumentNullExceptionHelpers.ThrowIfNull((argument1, nameof(argument1)), (argument2, nameof(argument2)), (argument3, nameof(argument3)));
        action.Should().Throw<ArgumentNullException>().WithMessage(ExceptionConstants.ValueCannotBeNull(expectedParameterName));
    }
}
