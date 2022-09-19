using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Configurations;

public sealed class HangfireSettingsConfiguration
{
    private readonly string _userName = null!;
    private readonly string _password = null!;

    public string UserName
    {
        get => _userName;
        init => _userName = value ?? throw new ArgumentNullException(nameof(UserName), ExceptionConstants.ArgumentNullException);
    }

    public string Password
    {
        get => _password;
        init => _password = value ?? throw new ArgumentNullException(nameof(Password), ExceptionConstants.ArgumentNullException);
    }
}
