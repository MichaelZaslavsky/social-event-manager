using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiConstants.FirstVersion, new OpenApiInfo
                {
                    Version = ApiConstants.FirstVersion,
                    Title = ApiConstants.SocialEventManagerApi,
                });
            });

            return services;
        }
    }
}
