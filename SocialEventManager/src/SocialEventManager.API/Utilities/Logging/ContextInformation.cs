namespace SocialEventManager.API.Utilities.Logging;

public record ContextInformation
{
    public string Host { get; init; }

    public string Method { get; init; }

    public string RemoteIpAddress { get; init; }

    public string Protocol { get; init; }

    public UserInformation UserInfo { get; init; }
}
