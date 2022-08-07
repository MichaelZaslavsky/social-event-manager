using System.Net;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialEventManager.Infrastructure.Attributes;
using SocialEventManager.Tests.Common.Helpers;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.AttributeTests;

public class ValidationFilterAttributeTests
{
    [Fact]
    public async Task Init_Should_ReturnNullResult_When_ArgumentsAreValid()
    {
        ValidationFilterAttribute validationFilter = new();
        ActionExecutingContext context = ActionContextHelpers.GetMockActionExecutingContext();

        await validationFilter.OnActionExecutionAsync(context, ActionContextHelpers.GetMockActionExecutionDelegate());
        context.Result.Should().BeNull();
    }

    [Theory]
    [AutoData]
    public async Task Init_Should_ReturnUnprocessableEntity_When_ModelHasErrors(string key, string errorMessage)
    {
        ValidationFilterAttribute validationFilter = new();

        ActionExecutingContext context = ActionContextHelpers.GetMockActionExecutingContext();
        context.ModelState.AddModelError(key, errorMessage);

        await validationFilter.OnActionExecutionAsync(context, ActionContextHelpers.GetMockActionExecutionDelegate());

        context.Result.Should().NotBeNull()
            .And.BeOfType<UnprocessableEntityObjectResult>();

        UnprocessableEntityObjectResult result = (context.Result as UnprocessableEntityObjectResult)!;
        result.StatusCode.Should().Be((int)HttpStatusCode.UnprocessableEntity);

        result.Value.Should().BeOfType<SerializableError>();
        SerializableError error = (result.Value as SerializableError)!;
        error.Keys.Should().HaveCount(1);
        error.Keys.Single().Should().Be(key);
        error.Values.Should().HaveCount(1);

        object valuesObj = error.Values.Single();
        string[] values = (valuesObj as string[])!;
        values.Should().HaveCount(1);
        values.Single().Should().Be(errorMessage);
    }
}
