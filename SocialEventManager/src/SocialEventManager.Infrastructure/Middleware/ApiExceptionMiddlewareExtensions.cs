using Microsoft.AspNetCore.Builder;

namespace SocialEventManager.Infrastructure.Middleware
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder, Action<ApiExceptionOptions> configureOptions)
        {
            ApiExceptionOptions options = new();
            configureOptions(options);

            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }
    }
}
