namespace SocialEventManager.Shared.Exceptions;

public class UnprocessableEntityException : ApplicationException
{
    public UnprocessableEntityException()
    {
    }

    public UnprocessableEntityException(string message)
        : base(message)
    {
    }

    public UnprocessableEntityException(string name, object key)
        : base($"{name} ({key}) is unprocessable")
    {
    }

    public UnprocessableEntityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
