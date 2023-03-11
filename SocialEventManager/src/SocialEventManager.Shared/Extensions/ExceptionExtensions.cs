using System.Net;
using FluentValidation;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Exceptions;

namespace SocialEventManager.Shared.Extensions;

public static class ExceptionExtensions
{
    public static (HttpStatusCode httpStatusCode, string title) ToHttpStatusCodeAndTitle(this Exception? ex)
    {
        if (ex is HttpException httpException)
        {
            return (httpException.StatusCode, httpException.Title);
        }

        return ex is ValidationException
            ? (HttpStatusCode.BadRequest, ExceptionConstants.BadRequest)
            : (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError);
    }

    public static bool IsCritical(this Exception? ex)
    {
        if (ex is null)
        {
            return false;
        }

        switch (ex)
        {
            case OutOfMemoryException:
            case AppDomainUnloadedException:
            case BadImageFormatException:
            case CannotUnloadAppDomainException:
            case InvalidProgramException:
            case ThreadAbortException:
            case { } when ex.Message.StartsWith(ExceptionConstants.CannotOpenDatabase, StringComparison.InvariantCultureIgnoreCase):
            case { } when ex.Message.StartsWith(ExceptionConstants.ANetworkRelated, StringComparison.InvariantCultureIgnoreCase):
                return true;

            default:
                return false;
        }
    }
}
