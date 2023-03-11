using System.Net;

namespace SocialEventManager.Shared.Exceptions;

public abstract class HttpException : ApplicationException
{
    public abstract HttpStatusCode StatusCode { get; init; }

    public abstract string Title { get; init; }

    protected HttpException()
    {
    }

    protected HttpException(string message)
        : base(message)
    {
    }

    protected HttpException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
