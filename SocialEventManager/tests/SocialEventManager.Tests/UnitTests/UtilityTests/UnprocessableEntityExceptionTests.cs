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
public sealed class UnprocessableEntityExceptionTests
{
    [Theory]
    [AutoData]
    public void InitUnprocessableEntityException_Should_Succeed_When_MessageIsValid(string message)
    {
        UnprocessableEntityException ex = new(message);
        ex.Message.Should().Be(message);
        ex.HResult.Should().Be(TestConstants.ApplicationExceptionHResult);
        ex.InnerException.Should().BeNull();
    }

    [Theory]
    [AutoData]
    public void InitUnprocessableEntityException_Should_Succeed_When_MessageAndInnerExceptionAreValid(string message, Exception innerException)
    {
        UnprocessableEntityException ex = new(message, innerException);
        ex.Message.Should().Be(message);
        ex.HResult.Should().Be(TestConstants.ApplicationExceptionHResult);
        ex.InnerException.Should().Be(innerException);
    }
}
