namespace SocialEventManager.Infrastructure.Middleware
{
    public class ApiErrorData
    {
        public ApiErrorData(string detail)
        {
            Detail = detail;
        }

        public ApiErrorData(string detail, string links)
            : this(detail)
        {
            Links = links;
        }

        public string Detail { get; set; }

        public string Links { get; set; }
    }
}
