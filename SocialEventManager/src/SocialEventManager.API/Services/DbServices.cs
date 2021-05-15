using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.DLL.Infrastructure;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.Services
{
    public static class DbServices
    {
        public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnectionFactory, SqlServerDbConnectionFactory>(_ =>
                new SqlServerDbConnectionFactory(configuration.GetConnectionString(DbConstants.DefaultConnection)));
        }
    }
}
