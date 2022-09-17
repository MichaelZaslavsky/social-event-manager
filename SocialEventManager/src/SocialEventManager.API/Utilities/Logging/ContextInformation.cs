namespace SocialEventManager.API.Utilities.Logging;

public sealed record ContextInformation
{
    public string? Host { get; init; }

    public string Method { get; init; } = null!;

    public string? RemoteIpAddress { get; init; }

    public string Protocol { get; init; } = null!;

    public UserInformation? UserInfo { get; init; }
}
