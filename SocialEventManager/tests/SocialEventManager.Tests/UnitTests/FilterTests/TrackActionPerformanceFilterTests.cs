using FluentAssertions;
using Microsoft.Extensions.Logging;
using SocialEventManager.Infrastructure.Filters;
using SocialEventManager.Tests.Common.Helpers;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.FilterTests;

public class TrackActionPerformanceFilterTests
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
