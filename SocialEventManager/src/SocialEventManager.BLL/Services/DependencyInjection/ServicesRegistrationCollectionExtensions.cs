using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.BLL.Services.DependencyInjection;

public static class ServicesRegistrationCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
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

        return services;
    }
}
