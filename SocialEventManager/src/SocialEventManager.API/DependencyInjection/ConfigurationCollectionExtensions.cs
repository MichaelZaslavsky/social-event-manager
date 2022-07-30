using SocialEventManager.API.Configurations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection;

public static class ConfigurationCollectionExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<BasicAuthenticationConfiguration>(config.GetSection(AuthConstants.BasicAuthentication));

        return services;
    }
}
