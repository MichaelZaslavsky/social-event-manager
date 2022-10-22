using ServiceStack;
using ServiceStack.Redis;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.API.DependencyInjection;

public static class MemoryCacheServiceCollectionExtensions
{
    public static IServiceCollection AddRedisClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IRedisClientsManagerAsync>(_ =>
        {
            string[]? hosts = config.GetSection(ApiConstants.Redis).Get<string[]>();
            ConfigurationHelpers.ThrowIfNull(hosts, ApiConstants.Redis);

            return new RedisManagerPool(hosts);
        });

        return services;
    }
}
