using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Helpers;

public static class ConfigurationHelpers
{
    public static void ThrowIfNull<T>([NotNull] T? argument, string key)
        where T : class
    {
        if (argument is null)
        {
            throw new ConfigurationErrorsException(ExceptionConstants.ConfigurationKeyIsMissing(key));
        }
    }
}
