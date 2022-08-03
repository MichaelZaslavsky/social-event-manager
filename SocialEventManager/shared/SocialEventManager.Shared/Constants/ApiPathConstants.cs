namespace SocialEventManager.Shared.Constants;

public static class ApiPathConstants
{
    public const string ApiController = Api + Controller;
    public const string ChatHub = "/chathub";
    public const string Contact = Api + "contact";
    public const string Hangfire = "/hangfire";
    public const string HealthLive = $"/{Health}/live";
    public const string HealthReady = $"/{Health}/{Ready}";
    public const string Ready = "ready";
    public const string Roles = Api + "roles";

    private const string Api = "api/";
    private const string Controller = "[controller]";
    private const string Health = "health";
}
