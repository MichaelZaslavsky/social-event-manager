using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class BackgroundJobsServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration config)
        {
            services.AddHangfire(h => h.UseSqlServerStorage(config.GetConnectionString(DbConstants.SocialEventManagerHangfire)));
            services.AddHangfireServer();

            return services;
        }
    }
}
