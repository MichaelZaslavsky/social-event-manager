namespace SocialEventManager.Shared.Exceptions;

public class NotFoundException : ApplicationException
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
}
