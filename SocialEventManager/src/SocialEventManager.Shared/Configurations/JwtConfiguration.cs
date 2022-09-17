using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Configurations;

public sealed class JwtConfiguration
{
    private readonly string _key = null!;
    private readonly string _expiryInDays = null!;
    private readonly string _issuer = null!;
    private readonly string _audience = null!;

    public string Key
    {
        get => _key;
        init => _key = value ?? throw new ArgumentNullException(nameof(value), ExceptionConstants.ArgumentNullException);
    }

    public string ExpiryInDays
    {
        get => _expiryInDays;
        init => _expiryInDays = value ?? throw new ArgumentNullException(nameof(value), ExceptionConstants.ArgumentNullException);
    }

    public string Issuer
    {
        get => _issuer;
        init => _issuer = value ?? throw new ArgumentNullException(nameof(value), ExceptionConstants.ArgumentNullException);
    }

    public string Audience
    {
        get => _audience;
        init => _audience = value ?? throw new ArgumentNullException(nameof(value), ExceptionConstants.ArgumentNullException);
    }
}
