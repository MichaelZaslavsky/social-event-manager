using FluentAssertions;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Tests.Common.Constants;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.Configurations;

[UnitTest]
[Category(nameof(UnitTests))]
public class JwtConfigurationTests
{
    [Theory]
    [InlineData(null, TestConstants.SomeText, TestConstants.MoreText, TestConstants.LoremIpsum, nameof(JwtConfiguration.Key))]
    [InlineData(TestConstants.SomeText, null, TestConstants.MoreText, TestConstants.LoremIpsum, nameof(JwtConfiguration.ExpiryInDays))]
    [InlineData(TestConstants.SomeText, TestConstants.MoreText, null, TestConstants.LoremIpsum, nameof(JwtConfiguration.Issuer))]
    [InlineData(TestConstants.SomeText, TestConstants.MoreText, TestConstants.LoremIpsum, null, nameof(JwtConfiguration.Audience))]
    public void InitFields_Should_ThrowArgumentNullException_When_DataIsInvalid(string key, string expiryInDays, string issuer, string audience, string expectedParamName)
    {
        Action action = () => new JwtConfiguration
        {
            Key = key,
            ExpiryInDays = expiryInDays,
            Issuer = issuer,
            Audience = audience,
        };

        action.Should().Throw<ArgumentNullException>().WithMessage(TestExceptionConstants.ArgumentNullException(expectedParamName));
    }
}
