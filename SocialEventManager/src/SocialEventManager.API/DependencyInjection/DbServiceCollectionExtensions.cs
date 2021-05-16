using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.DLL.Infrastructure;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class DbServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IDbConnectionFactory, SqlServerDbConnectionFactory>(_ =>
                new SqlServerDbConnectionFactory(config.GetConnectionString(DbConstants.DefaultConnection)));

            return services;
        }
    }
}
