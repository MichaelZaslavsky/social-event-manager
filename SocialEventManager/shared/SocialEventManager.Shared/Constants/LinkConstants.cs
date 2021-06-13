namespace SocialEventManager.Shared.Constants
{
    public static class LinkConstants
    {
        public const string BadRequestException = WikiHttpStatusUrl + "#400";
        public const string NotFoundException = WikiHttpStatusUrl + "#404";
        public const string NullReferenceException = "https://docs.microsoft.com/en-us/dotnet/api/system.nullreferenceexception?view=net-5.0";
        public const string ArgumentNullException = "https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception.-ctor?view=net-5.0";
        public const string ArgumentException = "https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception?view=net-5.0";
        public const string TimeoutException = "https://docs.microsoft.com/en-us/dotnet/api/system.timeoutexception?view=net-5.0";
        public const string SqlException = "https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlexception?view=dotnet-plat-ext-5.0";
        public const string Exception = "https://docs.microsoft.com/en-us/dotnet/api/system.exception?view=net-5.0";

        private const string WikiHttpStatusUrl = "https://en.wikipedia.org/wiki/List_of_HTTP_status_codes";
    }
}
