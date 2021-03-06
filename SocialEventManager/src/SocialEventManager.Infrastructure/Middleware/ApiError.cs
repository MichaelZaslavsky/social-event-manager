namespace SocialEventManager.Infrastructure.Middleware;

public class ApiError
{
    public string Id { get; set; } = null!;

    public short Status { get; set; }

    public string Title { get; set; } = null!;

    public ApiErrorData? Data { get; set; }
}
