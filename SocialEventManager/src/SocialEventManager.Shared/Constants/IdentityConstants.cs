namespace SocialEventManager.Shared.Constants;

public static class IdentityConstants
{
    public const string AspNet = nameof(AspNet);

    public const int MaxPasswordLength = 50;
    public const int MinPasswordLength = 8;
    public const int MaxEmailLength = 256;
    public const int MinEmailLength = 3;
    public const int MaxFailedAccessAttempts = 3;
}
