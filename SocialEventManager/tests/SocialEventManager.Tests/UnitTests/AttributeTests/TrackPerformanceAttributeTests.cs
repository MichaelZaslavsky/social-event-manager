using FluentAssertions;
using Microsoft.Extensions.Logging;
using SocialEventManager.Infrastructure.Attributes;
using SocialEventManager.Tests.Common.Helpers;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.AttributeTests;

public class TrackPerformanceAttributeTests
{
    private readonly ILogger<TrackPerformanceAttribute> _logger;

    public TrackPerformanceAttributeTests(ILogger<TrackPerformanceAttribute> logger)
    {
        _logger = logger;
    }

    [Fact]
    public void OnActionExecution_Should_NotThrowException_When_Called()
    {
        TrackPerformanceAttribute trackPerformance = new(_logger);

        Exception? executingException = Record.Exception(() => trackPerformance.OnActionExecuting(ActionContextHelpers.GetMockActionExecutingContext()));
        executingException.Should().BeNull();

        Exception? executedException = Record.Exception(() => trackPerformance.OnActionExecuted(ActionContextHelpers.GetMockActionExecutedContext()));
        executedException.Should().BeNull();
    }
}
