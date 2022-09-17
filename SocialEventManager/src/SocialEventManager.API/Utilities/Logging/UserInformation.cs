namespace SocialEventManager.API.Utilities.Logging;

public sealed record UserInformation
{
    public string? UserId { get; init; }

    public string? UserName { get; init; }

    public IDictionary<string, IList<string>> UserClaims { get; init; } = new Dictionary<string, IList<string>>();
}
