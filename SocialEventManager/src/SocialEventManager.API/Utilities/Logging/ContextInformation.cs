namespace SocialEventManager.API.Utilities.Logging;

public sealed record ContextInformation
{
    public string? Host { get; init; }

    required public string Method { get; init; }

    public string? RemoteIpAddress { get; init; }

    required public string Protocol { get; init; }

    public UserInformation? UserInfo { get; init; }
}
