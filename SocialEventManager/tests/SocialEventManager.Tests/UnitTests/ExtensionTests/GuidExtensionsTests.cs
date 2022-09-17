using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests;

[UnitTest]
[Category(CategoryConstants.Extensions)]
public sealed class GuidExtensionsTests
{
    private const int Base64LengthWithoutEquals = 22;
    private const string EmptyBase64 = "AAAAAAAAAAAAAAAAAAAAAA";

    [Theory]
    [AutoData]
    public void EncodeBase64String_DecodeBase64String_Should_ReturnInitialGuid_When_Called(Guid guid)
    {
        string actualBase64 = guid.EncodeBase64String();
        actualBase64.Should().NotBe(string.Empty)
            .And.HaveLength(Base64LengthWithoutEquals);

        Guid actualGuid = ((ReadOnlySpan<char>)actualBase64).DecodeBase64String();
        actualGuid.Should().Be(guid);
    }

    [Theory]
    [InlineData(EmptyBase64)]
    public void EncodeBase64String_Should_ReturnEmptyBase64_When_GuidIsEmpty(string expected)
    {
        string actualBase64 = Guid.Empty.EncodeBase64String();
        actualBase64.Should().Be(expected);
    }

    [Theory]
    [InlineData(EmptyBase64)]
    public void DecodeBase64String_Should_ReturnEmptyGuid_When_StringIsEmptyBase64(string base64)
    {
        Guid actual = ((ReadOnlySpan<char>)base64).DecodeBase64String();
        actual.Should().Be(Guid.Empty);
    }
}
