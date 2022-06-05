using System.Data.SqlClient;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using SocialEventManager.Infrastructure.Middleware;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.API.Utilities.Handlers;

public static class ErrorResponseHandler
{
    public static void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
    {
        string name = ex.GetType().Name;

        error.Data = name switch
        {
            nameof(ValidationException) => new ApiErrorData(JsonConvert.SerializeObject(((ValidationException)ex).ValdationErrors), LinkConstants.UnprocessableEntity),
            nameof(BadRequestException) => new ApiErrorData(ex.Message, LinkConstants.BadRequestException),
            nameof(NotFoundException) => new ApiErrorData(ex.Message, LinkConstants.NotFoundException),
            nameof(NullReferenceException) => new ApiErrorData(ExceptionConstants.NullReferenceException, LinkConstants.NullReferenceException),
            nameof(ArgumentNullException) => new ApiErrorData(ExceptionConstants.ArgumentNullException, LinkConstants.ArgumentNullException),
            nameof(ArgumentException) => new ApiErrorData(ExceptionConstants.ArgumentException, LinkConstants.ArgumentException),
            nameof(TimeoutException) => new ApiErrorData(ExceptionConstants.TimeoutException, LinkConstants.TimeoutException),
            nameof(SqlException) => new ApiErrorData(ExceptionConstants.SqlException, LinkConstants.SqlException),
            nameof(Exception) => new ApiErrorData(ExceptionConstants.Exception, LinkConstants.Exception),
            _ => new ApiErrorData(name)
        };

        string requestMessage = MessageHelpers.BuildRequestMessage(context.Request).Replace(Environment.NewLine, string.Empty);
        string responseMessage = MessageHelpers.BuildResponseMessage(error).Replace(Environment.NewLine, string.Empty);

        Log.Logger.Information(responseMessage + Environment.NewLine + Environment.NewLine + requestMessage);
    }

    public static LogEventLevel DetermineLogLevel(Exception ex)
    {
        return ex.IsCritical()
            ? LogEventLevel.Fatal
            : LogEventLevel.Error;
    }
}
