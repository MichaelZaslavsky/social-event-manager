namespace SocialEventManager.Shared.Exceptions;

public sealed class BadRequestException : ApplicationException
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
}
