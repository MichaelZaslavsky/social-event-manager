using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SocialEventManager.Infrastructure.Middleware;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.API.Utilities.Handlers
{
    public static class ErrorResponseHandler
    {
        public static void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
        {
            string name = ex.GetType().Name;

            error.Data = name switch
            {
                nameof(NullReferenceException) => new ApiErrorData(ExceptionConstants.NullReferenceException, LinkConstants.NullReferenceException),
                nameof(TimeoutException) => new ApiErrorData(ExceptionConstants.TimeoutException, LinkConstants.TimeoutException),
                nameof(SqlException) => new ApiErrorData(ExceptionConstants.SqlException, LinkConstants.SqlException),
                nameof(Exception) => new ApiErrorData(ExceptionConstants.Exception, LinkConstants.Exception),
                _ => new ApiErrorData(name)
            };
        }

        public static LogLevel DetermineLogLevel(Exception ex)
        {
            return ex.IsCritical()
                ? LogLevel.Critical
                : LogLevel.Error;
        }
    }
}
