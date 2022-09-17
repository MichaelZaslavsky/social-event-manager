using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.DataMembers.Common;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests;

[UnitTest]
[Category(CategoryConstants.Extensions)]
public sealed class StringExtensionsTests
{
    [Theory]
    [MemberData(nameof(StringData.NullOrEmptyData), MemberType = typeof(StringData))]
    public void IsNullOrEmpty_Should_ReturnCorrectResult_When_Called(string? value, bool expectedResult)
    {
        bool actualResult = value.IsNullOrEmpty();
        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(StringData.NullOrEmptyData), MemberType = typeof(StringData))]
    [MemberData(nameof(StringData.WhiteSpaceData), MemberType = typeof(StringData))]
    public void IsNullOrWhiteSpace_Should_ReturnCorrectResult_When_Called(string? value, bool expectedResult)
    {
        bool actualResult = value.IsNullOrWhiteSpace();
        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(StringData.TakeAfterData), MemberType = typeof(StringData))]
    [MemberData(nameof(StringData.TakeAfterFirstData), MemberType = typeof(StringData))]
    public void TaskAfterFirst_Should_ReturnPartialValue_When_ValueExists(string value, string? fromStart, StringComparison comparisonType, string expectedValue)
    {
        string actualResult = value.TakeAfterFirst(fromStart, comparisonType);
        actualResult.Should().Be(expectedValue);
    }

    [Theory]
    [MemberData(nameof(StringData.TakeAfterData), MemberType = typeof(StringData))]
    [MemberData(nameof(StringData.TakeAfterLastData), MemberType = typeof(StringData))]
    public void TaskAfterLast_Should_ReturnPartialValue_When_ValueExists(string value, string? fromStart, StringComparison comparisonType, string expectedValue)
    {
        string actualResult = value.TakeAfterLast(fromStart, comparisonType);
        actualResult.Should().Be(expectedValue);
    }

    [Theory]
    [MemberData(nameof(StringData.TakeUntilData), MemberType = typeof(StringData))]
    [MemberData(nameof(StringData.TakeUntilFirstData), MemberType = typeof(StringData))]
    public void TaskUntilFirst_Should_ReturnPartialValue_When_ValueExists(string value, string? fromEnd, StringComparison comparisonType, string expectedValue)
    {
        string actualResult = value.TakeUntilFirst(fromEnd, comparisonType);
        actualResult.Should().Be(expectedValue);
    }

    [Theory]
    [MemberData(nameof(StringData.TakeUntilData), MemberType = typeof(StringData))]
    [MemberData(nameof(StringData.TakeUntilLastData), MemberType = typeof(StringData))]
    public void TaskUntilLast_Should_ReturnPartialValue_When_ValueExists(string value, string? fromEnd, StringComparison comparisonType, string expectedValue)
    {
        string actualResult = value.TakeUntilLast(fromEnd, comparisonType);
        actualResult.Should().Be(expectedValue);
    }

    [Theory]
    [AutoData]
    public void Encode_ReturnsEncodedValue_WhenCalled(string input)
    {
        string actual = input.Encode();
        actual.Should().NotBeNullOrWhiteSpace();
        actual.Should().NotBe(input);
    }

    [Theory]
    [InlineData("aW5wdXQyZGI2NmMzMS00OGExLTQwZDYtOTc4Mi1hMjdlNmNjMWY1NDE")]
    public void Decode_ReturnsDecodedValue_WhenCalled(string input)
    {
        string actual = input.Decode();
        actual.Should().NotBeNullOrWhiteSpace();
        actual.Should().NotBe(input);
    }

    [Theory]
    [AutoData]
    public void EncodeAndDecode_ReturnsInitialInput_WhenCalled(string input)
    {
        string actual = input
            .Encode()
            .Decode();

        actual.Should().Be(input);
    }
}
