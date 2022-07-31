using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.HelperTests;

[UnitTest]
[Category(CategoryConstants.Helpers)]
public class RandomGeneratorHelpersTests
{
    [Theory]
    [AutoData]
    public void GenerateRandomValue_Should_ReturnValueOfRequestedLength_When_LengthIsPositive(int length)
    {
        string value = RandomGeneratorHelpers.GenerateRandomValue(length);
        value.Length.Should().Be(length);
    }

    [Fact]
    public void NextInt32_Should_Succeed()
    {
        Exception? exception = Record.Exception(() => RandomGeneratorHelpers.NextInt32());
        exception.Should().BeNull();
    }
}
