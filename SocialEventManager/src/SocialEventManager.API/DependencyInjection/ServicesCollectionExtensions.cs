using AspNetCoreRateLimit;
using SocialEventManager.BLL.Services.DependencyInjection;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Infrastructure.Loggers;

namespace SocialEventManager.API.DependencyInjection;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services.RegisterServices()
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddScoped<IEmailProvider, EmailSmtpProvider>()
            .AddSingleton<IScopeInformation, ScopeInformation>()
            .AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        return services;
    }
}
