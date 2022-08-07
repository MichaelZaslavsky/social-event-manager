using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Moq;
using SocialEventManager.Infrastructure.Filters;
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
    public void Init_Should_NotThrowException_When_Called()
    {
        TrackActionPerformanceFilter trackPerformance = new(_logger);
        ActionExecutedContext context = GetMockActionExecutedContext();

        Exception? executingException = Record.Exception(() => trackPerformance.OnActionExecuting(default!));
        executingException.Should().BeNull();

        Exception? executedException = Record.Exception(() => trackPerformance.OnActionExecuted(context));
        executedException.Should().BeNull();
    }

    private static ActionExecutedContext GetMockActionExecutedContext()
    {
        ActionContext actionContext = new(new DefaultHttpContext(), new(), Mock.Of<ActionDescriptor>());
        return new(actionContext, filters: new List<IFilterMetadata>(), controller: new());
    }
}
