using System.Net;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Exceptions;

public sealed class UnprocessableEntityException : HttpException
{
    public UnprocessableEntityException()
    {
    }

    public UnprocessableEntityException(string message)
        : base(message)
    {
    }

    public UnprocessableEntityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public override HttpStatusCode StatusCode { get; init; } = HttpStatusCode.UnprocessableEntity;

    public override string Title { get; init; } = ExceptionConstants.UnprocessableEntity;
}
