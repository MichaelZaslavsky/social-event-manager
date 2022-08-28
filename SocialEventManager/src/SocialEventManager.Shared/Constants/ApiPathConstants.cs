namespace SocialEventManager.Shared.Constants;

public static class ApiPathConstants
{
    public const string Accounts = Api + "accounts";
    public const string Action = "[action]";
    public const string ApiController = Api + Controller;
    public const string ChatHub = "/chathub";
    public const string ConfirmEmail = "confirm-email";
    public const string Contact = Api + "contact";
    public const string Hangfire = "/hangfire";
    public const string HealthLive = $"/{Health}/live";
    public const string HealthReady = $"/{Health}/{Ready}";
    public const string LoginAccounts = Accounts + "/login";
    public const string LogoutAccounts = Accounts + "/logout";
    public const string Ready = "ready";
    public const string RegisterAccounts = Accounts + "/register";
    public const string ResetPassword = "reset-password";
    public const string Roles = Api + "roles";

    private const string Api = "api/";
    private const string Controller = "[controller]";
    private const string Health = "health";
}
