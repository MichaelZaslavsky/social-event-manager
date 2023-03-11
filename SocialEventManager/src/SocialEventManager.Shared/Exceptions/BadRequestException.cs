using System.Net;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Exceptions;

public sealed class BadRequestException : HttpException
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public override HttpStatusCode StatusCode { get; init; } = HttpStatusCode.BadRequest;

    public override string Title { get; init; } = ExceptionConstants.BadRequest;
}
