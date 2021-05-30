using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
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
                Assembly.Load($"{nameof(SocialEventManager)}.{nameof(DLL)}"),
            };

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(c => c.Name.EndsWith(GlobalConstants.Service) || c.Name.EndsWith(GlobalConstants.Repository))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.AddSingleton<IScopeInformation, ScopeInformation>();

            return services;
        }
    }
}
