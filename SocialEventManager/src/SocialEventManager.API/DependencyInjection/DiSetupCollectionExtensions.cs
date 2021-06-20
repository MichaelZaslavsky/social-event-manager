using System.Reflection;
using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Infrastructure.Loggers;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class DiSetupCollectionExtensions
    {
        public static IServiceCollection RegisterDi (this IServiceCollection services)
        {
            Assembly[] assembliesToScan = new[]
            {
                Assembly.GetExecutingAssembly(),
                Assembly.Load($"{nameof(SocialEventManager)}.{nameof(BLL)}"),
                Assembly.Load($"{nameof(SocialEventManager)}.{nameof(DAL)}"),
            };

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(c => c.Name.EndsWith(GlobalConstants.Service) || c.Name.EndsWith(GlobalConstants.Repository))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IScopeInformation, ScopeInformation>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }
    }
}
