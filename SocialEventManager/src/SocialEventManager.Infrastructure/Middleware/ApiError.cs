namespace SocialEventManager.Infrastructure.Middleware
{
    public class ApiError
    {
        public string Id { get; set; }

        public short Status { get; set; }

        public string Title { get; set; }

        public ApiErrorData Data { get; set; }
    }
}
