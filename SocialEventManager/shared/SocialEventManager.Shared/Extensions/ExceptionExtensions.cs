using System;
using System.Threading;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Shared.Extensions
{
    public static class ExceptionExtensions
    {
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
