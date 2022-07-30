using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.Constants;

internal static class TestConstants
{
    public const string DatabaseDependent = "Database dependent";
    public const string StorageDependent = "Storage dependent";

    public static string ValueCannotBeNullOrEmpty(string paramName) => $"{ExceptionConstants.ValueCannotBeNullOrEmpty} {Parameter(paramName)}";

    public static string ValueCannotBeNullOrWhiteSpace(string paramName) => $"{ExceptionConstants.ValueCannotBeNullOrWhiteSpace} {Parameter(paramName)}";

    private static string Parameter(string paramName) => $"(Parameter '{paramName}')";
}
