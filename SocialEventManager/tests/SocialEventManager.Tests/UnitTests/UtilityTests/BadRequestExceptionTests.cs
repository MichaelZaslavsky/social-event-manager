using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Tests.Common.Constants;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.UtilityTests;

[UnitTest]
[Category(CategoryConstants.Utilities)]
public sealed class BadRequestExceptionTests
{
    [Theory]
    [AutoData]
    public void InitBadRequestException_Should_Succeed_When_MessageIsValid(string message)
    {
        BadRequestException ex = new(message);
        ex.Message.Should().Be(message);
        ex.HResult.Should().Be(TestConstants.ApplicationExceptionHResult);
        ex.InnerException.Should().BeNull();
    }

    [Theory]
    [AutoData]
    public void InitBadRequestException_Should_Succeed_When_MessageAndInnerExceptionAreValid(string message, Exception innerException)
    {
        BadRequestException ex = new(message, innerException);
        ex.Message.Should().Be(message);
        ex.HResult.Should().Be(TestConstants.ApplicationExceptionHResult);
        ex.InnerException.Should().Be(innerException);
    }
}
