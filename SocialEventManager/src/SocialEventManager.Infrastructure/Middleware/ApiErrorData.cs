namespace SocialEventManager.Infrastructure.Middleware;

public sealed class ApiErrorData
{
    public ApiErrorData()
    {
    }

    public ApiErrorData(string links)
    {
        Links = links;
    }

    required public string Detail { get; init; }

    public string? Links { get; init; }
}
