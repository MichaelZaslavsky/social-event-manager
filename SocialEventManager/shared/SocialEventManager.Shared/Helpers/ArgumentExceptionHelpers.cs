using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.Shared.Helpers;

public static class ArgumentExceptionHelpers
{
    public static void ThrowIfNullOrEmpty<T>([NotNull] IEnumerable<T> argument, string paramName)
    {
        if (argument.IsNullOrEmpty())
        {
            throw new ArgumentException(ExceptionConstants.ValueCannotBeNullOrEmpty, paramName);
        }
    }

    public static void ThrowIfNullOrWhiteSpace([NotNull] string argument, string paramName)
    {
        if (argument.IsNullOrWhiteSpace())
        {
            throw new ArgumentException(ExceptionConstants.ValueCannotBeNullOrWhiteSpace, paramName);
        }
    }
}
