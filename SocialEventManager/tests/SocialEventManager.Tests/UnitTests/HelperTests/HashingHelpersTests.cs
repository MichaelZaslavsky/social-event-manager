using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.HelperTests;

[UnitTest]
[Category(CategoryConstants.Helpers)]
public sealed class HashingHelpersTests
{
    [Theory]
    [InlineData(TestConstants.SomeText, TestConstants.LoremIpsum)]
    public void RSHash_Should_ReturnHashedValue_When_Called(string value, string otherValue)
    {
        int actualValue = HashingHelpers.RSHash(value);
        int actualOtherValue = HashingHelpers.RSHash(otherValue);

        actualValue.Should().NotBe(actualOtherValue);
    }
}
