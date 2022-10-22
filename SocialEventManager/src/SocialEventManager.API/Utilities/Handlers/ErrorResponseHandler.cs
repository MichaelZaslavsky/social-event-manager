using System.Data.SqlClient;
using FluentValidation;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using SocialEventManager.Infrastructure.Middleware;
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
            nameof(ValidationException) => new ApiErrorData(LinkConstants.BadRequestException) { Detail = JsonConvert.SerializeObject(ex.Message) },
            nameof(BadRequestException) => new ApiErrorData(LinkConstants.BadRequestException) { Detail = ex.Message },
            nameof(NotFoundException) => new ApiErrorData(LinkConstants.NotFoundException) { Detail = ex.Message },
            nameof(UnprocessableEntityException) => new ApiErrorData(LinkConstants.UnprocessableEntity) { Detail = ex.Message },
            nameof(NullReferenceException) => new ApiErrorData(LinkConstants.NullReferenceException) { Detail = ExceptionConstants.NullReferenceException },
            nameof(ArgumentNullException) => new ApiErrorData(LinkConstants.ArgumentNullException) { Detail = ExceptionConstants.ArgumentNullException },
            nameof(ArgumentException) => new ApiErrorData(LinkConstants.ArgumentException) { Detail = ExceptionConstants.ArgumentException },
            nameof(TimeoutException) => new ApiErrorData(LinkConstants.TimeoutException) { Detail = ExceptionConstants.TimeoutException },
            nameof(SqlException) => new ApiErrorData(LinkConstants.SqlException) { Detail = ExceptionConstants.SqlException },
            nameof(Exception) => new ApiErrorData(LinkConstants.Exception) { Detail = ExceptionConstants.Exception },
            _ => new ApiErrorData() { Detail = name }
        };

        string? requestMessage = MessageHelpers.BuildRequestMessage(context.Request)?.Replace(Environment.NewLine, string.Empty);
        string? responseMessage = MessageHelpers.BuildResponseMessage(error)?.Replace(Environment.NewLine, string.Empty);

        Log.Logger.Information(responseMessage + Environment.NewLine + Environment.NewLine + requestMessage);
    }

    public static LogEventLevel DetermineLogLevel(Exception ex)
    {
        return ex.IsCritical()
            ? LogEventLevel.Fatal
            : LogEventLevel.Error;
    }
}
