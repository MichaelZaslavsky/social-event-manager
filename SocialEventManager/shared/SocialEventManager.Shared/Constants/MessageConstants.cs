namespace SocialEventManager.Shared.Constants;

public static class MessageConstants
{
    public const string InternalServerError = "Internal Server Error. Please try again later.";

    public static string ContactInfo(string name, string email) =>
        $"Contact us: user '{name}', email '{email}'";
}
