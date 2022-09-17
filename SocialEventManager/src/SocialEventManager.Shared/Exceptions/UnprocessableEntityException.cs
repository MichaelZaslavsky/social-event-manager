namespace SocialEventManager.Shared.Exceptions;

public sealed class UnprocessableEntityException : ApplicationException
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
}
