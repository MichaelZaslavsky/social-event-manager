namespace SocialEventManager.Shared.Constants;

public static class AuthConstants
{
    public const string Authorization = nameof(Authorization);
    public const string Bearer = "bearer";
    public const string Scheme = "bearer";
    public const string Jwt = "JWT";
    public const string SwaggerAuthenticationDescription = "Input your JWT token";

    public const string ConfirmEmailFailed = "Confirm email failed.";
    public const string EmailOrPasswordIsIncorrect = "The email address or password is incorrect.";
    public const string EmailNotVerified = "Invalid login attempt. Please verify your email address.";
    public const string ResetPasswordFailed = "Reset password failed.";
    public const string UserIsLocked = "Your account has been locked for 30 minutes. Please try again later.";
}
