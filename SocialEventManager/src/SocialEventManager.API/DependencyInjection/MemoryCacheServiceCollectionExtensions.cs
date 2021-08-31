using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;
using SocialEventManager.Infrastructure.Cache.Redis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class MemoryCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisClients(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IRedisClientsManager>(_ =>
            {
                string[] hosts = config.GetSection(ApiConstants.Redis).Get<string[]>();
                return new RedisManagerPool(hosts);
            });

            services.AddTransient<ICacheClient, RedisCacheClient>();

            return services;
        }
    }
}
