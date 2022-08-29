using Hangfire;
using Hangfire.Annotations;
using HangfireBasicAuthenticationFilter;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection;

public static class BackgroundJobsExtensions
{
    public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration config)
    {
        services.AddHangfire(h => h.UseSqlServerStorage(config.GetConnectionString(DbConstants.SocialEventManagerHangfire)));
        services.AddHangfireServer();

        return services;
    }

    public static IApplicationBuilder UseHangfireDashboard([NotNull] this IApplicationBuilder app, IConfiguration config)
    {
        HangfireSettingsConfiguration hangfireSettings = config
            .GetSection(ConfigurationConstants.HangfireSettings)
            .Get<HangfireSettingsConfiguration>();

        return app.UseHangfireDashboard(ApiPathConstants.Hangfire, new()
        {
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter()
                {
                    User = hangfireSettings.UserName,
                    Pass = hangfireSettings.Password,
                },
            },
            IgnoreAntiforgeryToken = true,
        });
    }
}
