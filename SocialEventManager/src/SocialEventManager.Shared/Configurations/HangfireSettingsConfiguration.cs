namespace SocialEventManager.Shared.Configurations;

public class HangfireSettingsConfiguration
{
    private readonly string _userName = null!;
    private readonly string _password = null!;

    public string UserName
    {
        get => _userName;
        init => _userName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Password
    {
        get => _password;
        init => _password = value ?? throw new ArgumentNullException(nameof(value));
    }
}
