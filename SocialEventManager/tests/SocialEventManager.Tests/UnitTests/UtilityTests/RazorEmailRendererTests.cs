using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.BLL.Services.Email;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Constants.Validations;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.UtilityTests;

[UnitTest]
[Category(CategoryConstants.Utilities)]
public class RazorEmailRendererTests
{
    private readonly IEmailRenderer _emailRenderer;

    public RazorEmailRendererTests(IEmailRenderer emailRenderer)
    {
        _emailRenderer = emailRenderer;
    }

    [Theory]
    [AutoData]
    public async Task RenderAsync_Should_ThrowInvalidOperationException_When_ViewIsNotFound(object model)
    {
        Func<Task> func = async () => await _emailRenderer.RenderAsync(model);

        string viewPath = $"~/EmailTemplates/{typeof(object).Name}.cshtml";
        string searchedLocations = viewPath;
        (await func.Should().ThrowAsync<InvalidOperationException>()).WithMessage(ValidationConstants.CouldNotFindThisView(viewPath, searchedLocations));
    }
}
