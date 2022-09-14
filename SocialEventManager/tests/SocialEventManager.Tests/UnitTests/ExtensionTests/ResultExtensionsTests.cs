using AutoFixture.Xunit2;
using FluentAssertions;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.DataMembers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests;

[UnitTest]
[Category(CategoryConstants.Extensions)]
public class ResultExtensionsTests
{
    [Theory]
    [AutoData]
    public void ToOk_Should_ReturnOk_When_ResultDoesNotHaveAnException(object obj)
    {
        Result<object> result = new(obj);
        IActionResult actual = result.ToOk(value => value);
        actual.Should().BeOfType(typeof(OkObjectResult));

        OkObjectResult okResult = (OkObjectResult)actual;
        okResult.Value.Should().Be(obj);
    }

    [Theory]
    [MemberData(nameof(ResultData.ResultDataWithExceptions), MemberType = typeof(ResultData))]
    public void ToOk_Should_NotReturnOk_When_ResultHasAnException(Result<object> result, Type expectedType, int expectedStatusCode)
    {
        IActionResult actual = result.ToOk(value => value);
        actual.Should().BeOfType(expectedType);

        ObjectResult objResult = (ObjectResult)actual;
        objResult.StatusCode.Should().Be(expectedStatusCode);
        objResult.Value.Should().Be(result.IfFail(value => value.Message));
    }

    [Fact]
    public void ToOk_Should_ReturnInternalServerError_When_ResultHasANonHandledException()
    {
        Result<object> result = new(new Exception());
        IActionResult actual = result.ToOk(value => value);
        actual.Should().BeOfType(typeof(StatusCodeResult));

        StatusCodeResult statusCodeResult = (StatusCodeResult)actual;
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
}
