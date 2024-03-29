using AspNetCoreRateLimit;
using MailKit.Net.Smtp;
using SocialEventManager.BLL.Services.DependencyInjection;
using SocialEventManager.BLL.Services.Email;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Infrastructure.Auth;
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
            .AddScoped<IEmailRenderer, RazorEmailRenderer>()
            .AddScoped<IJwtHandler, JwtHandler>()
            .AddScoped<ISmtpClient, SmtpClient>()
            .AddSingleton<IScopeInformation, ScopeInformation>()
            .AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        return services;
    }
}
