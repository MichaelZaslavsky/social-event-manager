using Microsoft.EntityFrameworkCore;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Infrastructure.Identity;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(
            opts => opts.UseSqlServer(
                config.GetConnectionString(DbConstants.SocialEventManager)!,
                s => s.MigrationsAssembly($"{nameof(SocialEventManager)}.{nameof(DAL)}")
                    .MigrationsHistoryTable(TableNameConstants.EntityFrameworkHistory, SchemaConstants.Migration)));

        return services;
    }

    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IDbSession, DbSession>(_ =>
            new DbSession(config.GetConnectionString(DbConstants.SocialEventManager)!));

        return services;
    }
}
