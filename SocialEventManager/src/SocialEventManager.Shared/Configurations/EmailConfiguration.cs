using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Configurations;

public sealed class EmailConfiguration
{
    private readonly string _userName = null!;
    private readonly string _password = null!;
    private readonly string _host = null!;

    public string UserName
    {
        get => _userName;
        init => _userName = value ?? throw new ArgumentNullException(nameof(value), ExceptionConstants.ArgumentNullException);
    }

    public string Password
    {
        get => _password;
        init => _password = value ?? throw new ArgumentNullException(nameof(value), ExceptionConstants.ArgumentNullException);
    }

    public string Host
    {
        get => _host;
        init => _host = value ?? throw new ArgumentNullException(nameof(value), ExceptionConstants.ArgumentNullException);
    }
}
