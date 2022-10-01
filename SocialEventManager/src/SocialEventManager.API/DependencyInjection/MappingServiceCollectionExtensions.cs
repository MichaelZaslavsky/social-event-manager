using Mapster;
using MapsterMapper;

namespace SocialEventManager.API.DependencyInjection;

public static class MappingServiceCollectionExtensions
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
