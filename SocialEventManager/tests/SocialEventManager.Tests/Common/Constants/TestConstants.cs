using Bogus;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.Constants;

internal static class TestConstants
{
    public const string TestingEnvironmentName = "Development";
    public const string DatabaseDependent = "Database dependent";
    public const string StorageDependent = "Storage dependent";
    public const string SomeText = "Some Text";
    public const string MoreText = "More Text";
    public const string LoremIpsum = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
    public const string ValidEmail = "valid-email@email-domain.com";
    public const string OtherValidEmail = "other-valid-email@email-domain.com";
    public const string ValidPassword = "123456Aa!";
    public const string ValidUserName = "ValidUserName";
    public const int ApplicationExceptionHResult = -2146232832;

    public const string ValidToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6Im1pY2hhZWwuemFzbGF2c2t5QGNsaWNrcmVwb3J0aW5nLmFpIiwiaHR0cDovL3NjaGVtYXMueG1sc" +
        "29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiZmRlYzdmY2ItMmIxZi00ZWRhLTgxYTAtMWM3NjUxY2ZhNjdjIiwiaGFzLXN1YnNjcmlwdGlvbiI6InRydWUiLCJleHAiOjE2NjEy" +
        "NzI2NDMsImlzcyI6ImFwaS5jbGlja3JlcG9ydGluZy5haSIsImF1ZCI6ImNsaWNrcmVwb3J0aW5nLmFpIn0.pjdsikg89IA2MK58igRP1w5XsW3EHTY5PIleWAmqRV4";

    public const string FailedToFindRegisteredServices = "Failed to find registered service/s for: ";

    public static string ValueCannotBeNullOrEmpty(string paramName) => $"{ExceptionConstants.ValueCannotBeNullOrEmpty} {Parameter(paramName)}";

    public static string ValueCannotBeNullOrWhiteSpace(string paramName) => $"{ExceptionConstants.ValueCannotBeNullOrWhiteSpace} {Parameter(paramName)}";

    private static string Parameter(string paramName) => $"(Parameter '{paramName}')";

    public static string Length256 => new Faker().Random.String(256);
}
