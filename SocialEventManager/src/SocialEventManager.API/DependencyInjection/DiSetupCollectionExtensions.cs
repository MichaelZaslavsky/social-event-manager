using AspNetCoreRateLimit;
using SocialEventManager.BLL.Services.DependencyInjection;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Infrastructure.Loggers;

namespace SocialEventManager.API.DependencyInjection
{
    public static class DiSetupCollectionExtensions
    {
        public static IServiceCollection RegisterDi(this IServiceCollection services)
        {
            services.RegisterServices()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddSingleton<IScopeInformation, ScopeInformation>()
                .AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }
    }
}
