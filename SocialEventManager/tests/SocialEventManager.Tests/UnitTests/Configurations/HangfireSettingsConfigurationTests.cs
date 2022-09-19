using FluentAssertions;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Tests.Common.Constants;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.Configurations;

[UnitTest]
[Category(nameof(UnitTests))]
public sealed class HangfireSettingsConfigurationTests
{
    [Theory]
    [InlineData(null, TestConstants.SomeText, nameof(HangfireSettingsConfiguration.UserName))]
    [InlineData(TestConstants.SomeText, null, nameof(HangfireSettingsConfiguration.Password))]
    public void InitFields_Should_ThrowArgumentNullException_When_DataIsInvalid(string userName, string password, string expectedParamName)
    {
        Action action = () => new HangfireSettingsConfiguration
        {
            UserName = userName,
            Password = password,
        };

        action.Should().Throw<ArgumentNullException>().WithMessage(TestExceptionConstants.ArgumentNullException(expectedParamName));
    }
}
