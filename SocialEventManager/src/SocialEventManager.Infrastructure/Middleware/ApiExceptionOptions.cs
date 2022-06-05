using Microsoft.AspNetCore.Http;
using Serilog.Events;

namespace SocialEventManager.Infrastructure.Middleware
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }

        public Func<Exception, LogEventLevel> DetermineLogLevel { get; set; }
    }
}
