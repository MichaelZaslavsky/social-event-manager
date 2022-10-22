using System.Configuration;
using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.HelperTests;

[UnitTest]
[Category(CategoryConstants.Helpers)]
public class ConfigurationHelpersTests
{
    [Theory]
    [AutoData]
    public void ThrowIfNull_Should_NotThrowArgumentException_When_ArgumentHasValue(object argument, string key)
    {
        Exception? exception = Record.Exception(() => ConfigurationHelpers.ThrowIfNull(argument, key));
        exception.Should().BeNull();
    }

    [Theory]
    [InlineData(ConfigurationConstants.AppUrl)]
    [InlineData(ConfigurationConstants.Email)]
    [InlineData(ConfigurationConstants.HangfireSettings)]
    [InlineData(ConfigurationConstants.Jwt)]
    public void ThrowIfNull_Should_ThrowConfigurationErrorsException_When_ArgumentIsNull(string key)
    {
        Action action = () => ConfigurationHelpers.ThrowIfNull((object?)null, key);
        action.Should().Throw<ConfigurationErrorsException>().WithMessage(ExceptionConstants.ConfigurationKeyIsMissing(key));
    }
}
