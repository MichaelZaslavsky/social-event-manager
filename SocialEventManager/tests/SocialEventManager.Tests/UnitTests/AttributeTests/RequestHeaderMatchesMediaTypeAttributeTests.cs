using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.API.Utilities.Attributes;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Constants;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.AttributeTests;

[UnitTest]
[Category(CategoryConstants.Attributes)]
public sealed class RequestHeaderMatchesMediaTypeAttributeTests
{
    [Theory]
    [InlineData(null, null, new string[] { }, "requestHeaderToMatch")]
    [InlineData(null, TestConstants.SomeText, new string[] { }, "requestHeaderToMatch")]
    [InlineData(TestConstants.SomeText, null, new string[] { }, "mediaType")]
    public void Init_Should_ThrowArgumentNullException_When_ArgumentsAreNull(
        string requestHeaderToMatch, string mediaType, string[] otherMediaTypes, string expected)
    {
        Action action = () => new RequestHeaderMatchesMediaTypeAttribute(requestHeaderToMatch, mediaType, otherMediaTypes);
        action.Should().Throw<ArgumentNullException>().WithMessage(ExceptionConstants.ValueCannotBeNull(expected));
    }

    [Theory]
    [AutoData]
    public void Init_Should_ThrowArgumentException_When_MediaTypeIsInvalid(string requestHeaderToMatch, string mediaType, string[] otherMediaTypes)
    {
        Action action = () => new RequestHeaderMatchesMediaTypeAttribute(requestHeaderToMatch, mediaType, otherMediaTypes);
        action.Should().Throw<ArgumentException>().WithMessage(TestExceptionConstants.ArgumentNullException(nameof(mediaType)));
    }

    [Theory]
    [InlineData(TestConstants.SomeText, MediaTypeConstants.ApplicationJson, new[] { TestConstants.SomeText })]
    public void Init_Should_ThrowArgumentException_When_OtherMediaTypesAreInvalid(string requestHeaderToMatch, string mediaType, string[] otherMediaTypes)
    {
        Action action = () => new RequestHeaderMatchesMediaTypeAttribute(requestHeaderToMatch, mediaType, otherMediaTypes);
        action.Should().Throw<ArgumentException>().WithMessage(TestExceptionConstants.ArgumentNullException(nameof(otherMediaTypes)));
    }

    [Theory]
    [InlineData(MediaTypeConstants.ApplicationXml, MediaTypeConstants.ApplicationJson, new[] { MediaTypeConstants.ApplicationXml })]
    public void Init_Should_CreateRequestHeaderMatchesInstance_When_DataIsValid(string requestHeaderToMatch, string mediaType, string[] otherMediaTypes)
    {
        RequestHeaderMatchesMediaTypeAttribute requestHeader = new(requestHeaderToMatch, mediaType, otherMediaTypes);
        requestHeader.Order.Should().Be(0);
    }
}
