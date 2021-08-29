using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SocialEventManager.API.DependencyInjection;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class HealthChecksServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddHealthChecks()
                .AddSqlServer(config.GetConnectionString(DbConstants.SocialEventManager), failureStatus: HealthStatus.Unhealthy, tags: new[] { "ready" });

            return services;
        }
    }
}
