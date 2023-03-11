using System.Net;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Exceptions;

public sealed class NotFoundException : HttpException
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public override HttpStatusCode StatusCode { get; init; } = HttpStatusCode.NotFound;

    public override string Title { get; init; } = ExceptionConstants.NotFound;
}
