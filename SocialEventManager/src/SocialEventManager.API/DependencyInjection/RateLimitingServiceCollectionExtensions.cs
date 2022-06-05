using AspNetCoreRateLimit;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class RateLimitingServiceCollectionExtensions
    {
        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<IpRateLimitOptions>(config.GetSection(ApiConstants.IpRateLimiting));
            services.Configure<IpRateLimitPolicies>(config.GetSection(ApiConstants.IpRateLimitPolicies));
            services.AddInMemoryRateLimiting();

            return services;
        }
    }
}
