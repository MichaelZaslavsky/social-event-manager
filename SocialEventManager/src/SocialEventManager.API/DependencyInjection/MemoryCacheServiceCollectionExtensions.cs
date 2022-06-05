using ServiceStack;
using ServiceStack.Redis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection;

public static class MemoryCacheServiceCollectionExtensions
{
    public static IServiceCollection AddRedisClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IRedisClientsManagerAsync>(_ =>
        {
            string[] hosts = config.GetSection(ApiConstants.Redis).Get<string[]>();
            return new RedisManagerPool(hosts);
        });

        return services;
    }
}
