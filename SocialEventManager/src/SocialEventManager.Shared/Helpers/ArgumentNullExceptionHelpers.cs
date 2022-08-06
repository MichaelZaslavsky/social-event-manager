namespace SocialEventManager.Shared.Helpers;

public static class ArgumentNullExceptionHelpers
{
    public static void ThrowIfNull(params (object? Argument, string ParamName)[] value)
    {
        foreach ((object? argument, string paramName) in value)
        {
            ArgumentNullException.ThrowIfNull(argument, paramName);
        }
    }
}
