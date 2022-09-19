using FluentAssertions;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Tests.Common.Constants;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.Configurations;

[UnitTest]
[Category(nameof(UnitTests))]
public sealed class EmailConfigurationTests
{
    [Theory]
    [InlineData(null, TestConstants.SomeText, TestConstants.MoreText, nameof(EmailConfiguration.UserName))]
    [InlineData(TestConstants.SomeText, null, TestConstants.MoreText, nameof(EmailConfiguration.Password))]
    [InlineData(TestConstants.SomeText, TestConstants.MoreText, null, nameof(EmailConfiguration.Host))]
    public void InitFields_Should_ThrowArgumentNullException_When_DataIsInvalid(string userName, string password, string host, string expectedParamName)
    {
        Action action = () => new EmailConfiguration
        {
            UserName = userName,
            Password = password,
            Host = host,
        };

        action.Should().Throw<ArgumentNullException>().WithMessage(TestExceptionConstants.ArgumentNullException(expectedParamName));
    }
}
