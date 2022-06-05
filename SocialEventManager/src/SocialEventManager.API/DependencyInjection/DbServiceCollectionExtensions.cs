using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IDbSession, DbSession>(_ =>
            new DbSession(config.GetConnectionString(DbConstants.SocialEventManager)));

        return services;
    }
}
