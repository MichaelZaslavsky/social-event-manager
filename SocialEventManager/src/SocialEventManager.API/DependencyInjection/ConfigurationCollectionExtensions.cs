using SocialEventManager.API.Configurations;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection;

public static class ConfigurationCollectionExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<BasicAuthenticationConfiguration>(config.GetSection(ConfigurationConstants.BasicAuthentication));
        services.Configure<EmailConfiguration>(config.GetSection(ConfigurationConstants.Email));

        return services;
    }
}
