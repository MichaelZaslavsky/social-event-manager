using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.Constants;

internal static class TestExceptionConstants
{
    public static string ArgumentNullException(string paramName) =>
        $"{ExceptionConstants.ArgumentNullException} (Parameter '{paramName}')";
}
