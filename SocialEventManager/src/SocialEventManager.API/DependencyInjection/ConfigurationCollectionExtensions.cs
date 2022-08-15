using SocialEventManager.Shared.Configurations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection;

public static class ConfigurationCollectionExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailConfiguration>(config.GetSection(ConfigurationConstants.Email));
        services.Configure<JwtConfiguration>(config.GetSection(ConfigurationConstants.Jwt));

        return services;
    }
}
