namespace SocialEventManager.Infrastructure.Middleware;

public sealed class ApiError
{
    required public string Id { get; init; }

    public short Status { get; init; }

    required public string Title { get; init; }

    public ApiErrorData? Data { get; set; }
}
