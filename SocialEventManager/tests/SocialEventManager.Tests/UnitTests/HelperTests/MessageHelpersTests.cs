using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.HelperTests;

[UnitTest]
[Category(CategoryConstants.Helpers)]
public sealed class MessageHelpersTests
{
    [Fact]
    public void BuildRequestMessage_Should_ReturnNull_When_HttpRequestIsNull()
    {
        string? message = MessageHelpers.BuildRequestMessage(null!);
        message.Should().BeNull();
    }

    [Fact]
    public void BuildResponseMessage_Should_ReturnNull_When_ApiErrorIsNull()
    {
        string? message = MessageHelpers.BuildResponseMessage(null!);
        message.Should().BeNull();
    }
}
