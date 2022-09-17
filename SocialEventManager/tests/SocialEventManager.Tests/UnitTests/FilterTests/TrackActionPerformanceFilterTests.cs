using FluentAssertions;
using Microsoft.Extensions.Logging;
using SocialEventManager.Infrastructure.Filters;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Helpers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.FilterTests;

[UnitTest]
[Category(CategoryConstants.Filters)]
public sealed class TrackActionPerformanceFilterTests
{
    private readonly ILogger<TrackActionPerformanceFilter> _logger;

    public TrackActionPerformanceFilterTests(ILogger<TrackActionPerformanceFilter> logger)
    {
        _logger = logger;
    }

    [Fact]
    public void OnActionExecuting_Should_NotThrowException_When_Called()
    {
        TrackActionPerformanceFilter trackPerformance = new(_logger);

        Exception? executingException = Record.Exception(() => trackPerformance.OnActionExecuting(ActionContextHelpers.GetMockActionExecutingContext()));
        executingException.Should().BeNull();

        Exception? executedException = Record.Exception(() => trackPerformance.OnActionExecuted(ActionContextHelpers.GetMockActionExecutedContext()));
        executedException.Should().BeNull();
    }
}
