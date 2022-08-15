using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.Common.Constants;

internal static class TestConstants
{
    public const string TestingEnvironmentName = "Development";
    public const string DatabaseDependent = "Database dependent";
    public const string StorageDependent = "Storage dependent";
    public const string SomeText = "Some Text";
    public const string LoremIpsum = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
    public const string ValidEmail = "valid-email@email-domain.com";
    public const string OtherValidEmail = "other-valid-email@email-domain.com";
    public const string ValidUserName = "ValidUserName";

    public static string ValueCannotBeNullOrEmpty(string paramName) => $"{ExceptionConstants.ValueCannotBeNullOrEmpty} {Parameter(paramName)}";

    public static string ValueCannotBeNullOrWhiteSpace(string paramName) => $"{ExceptionConstants.ValueCannotBeNullOrWhiteSpace} {Parameter(paramName)}";

    private static string Parameter(string paramName) => $"(Parameter '{paramName}')";

    public static string Length256 => RandomGeneratorHelpers.GenerateRandomValue(256);
}
