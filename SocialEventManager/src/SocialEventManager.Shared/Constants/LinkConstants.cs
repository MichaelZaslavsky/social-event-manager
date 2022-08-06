namespace SocialEventManager.Shared.Constants;

public static class LinkConstants
{
    public const string BadRequestException = $"{WikiHttpStatusUrl}#400";
    public const string NotFoundException = $"{WikiHttpStatusUrl}#404";
    public const string UnprocessableEntity = $"{WikiHttpStatusUrl}#422";
    public const string NullReferenceException = $"{MicrosoftApiUrl}system.nullreferenceexception?view=net-6.0";
    public const string ArgumentNullException = $"{MicrosoftApiUrl}system.argumentnullexception.-ctor?view=net-6.0";
    public const string ArgumentException = $"{MicrosoftApiUrl}system.argumentexception?view=net-6.0";
    public const string TimeoutException = $"{MicrosoftApiUrl}system.timeoutexception?view=net-6.0";
    public const string SqlException = $"{MicrosoftApiUrl}system.data.sqlclient.sqlexception?view=dotnet-plat-ext-6.0";
    public const string Exception = $"{MicrosoftApiUrl}system.exception?view=net-6.0";

    private const string WikiHttpStatusUrl = "https://en.wikipedia.org/wiki/List_of_HTTP_status_codes";
    private const string MicrosoftApiUrl = "https://docs.microsoft.com/en-us/dotnet/api/";
}
