using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SocialEventManager.Infrastructure.Loggers;

namespace SocialEventManager.Infrastructure.Filters;

public class TrackActionPerformanceFilter : IActionFilter
{
    private readonly ILogger<TrackActionPerformanceFilter> _logger;
    private Stopwatch _timer;

    public TrackActionPerformanceFilter(ILogger<TrackActionPerformanceFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _timer = new Stopwatch();
        _timer.Start();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _timer.Stop();

        if (context.Exception is null)
        {
            _logger.LogRoutePerformance(context.HttpContext.Request.Path, context.HttpContext.Request.Method, _timer.ElapsedMilliseconds);
        }
    }
}
