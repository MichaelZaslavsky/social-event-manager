using System;
using System.Net;
using System.Threading;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Exceptions;

namespace SocialEventManager.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static (HttpStatusCode httpStatusCode, string title) ToHttpStatusCodeAndTitle(this Exception ex)
        {
            if (ex == null)
            {
                return (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError);
            }

            return ex switch
            {
                NotFoundException => (HttpStatusCode.NotFound, ExceptionConstants.NotFound),
                BadRequestException or ValidationException => (HttpStatusCode.BadRequest, ExceptionConstants.BadRequest),
                _ => (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError),
            };
        }

        public static bool IsCritical(this Exception ex)
        {
            if (ex == null)
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
}
