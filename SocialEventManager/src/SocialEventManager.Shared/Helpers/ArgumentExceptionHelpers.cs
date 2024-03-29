using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.Shared.Helpers;

public static class ArgumentExceptionHelpers
{
    public static void ThrowIfNullOrEmpty<T>([NotNull] IEnumerable<T>? argument, [CallerArgumentExpression(nameof(argument))] string paramName = "")
    {
        if (argument.IsNullOrEmpty())
        {
            throw new ArgumentException(ExceptionConstants.ValueCannotBeNullOrEmpty, paramName);
        }
    }

    public static void ThrowIfNullOrWhiteSpace([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string paramName = "")
    {
        if (argument.IsNullOrWhiteSpace())
        {
            throw new ArgumentException(ExceptionConstants.ValueCannotBeNullOrWhiteSpace, paramName);
        }
    }
}
