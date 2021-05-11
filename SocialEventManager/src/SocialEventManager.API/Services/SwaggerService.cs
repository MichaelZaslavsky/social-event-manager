using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.Services
{
    public static class SwaggerService
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiConstants.FirstVersion, new OpenApiInfo
                {
                    Version = ApiConstants.FirstVersion,
                    Title = ApiConstants.SocialEventManagerApi,
                });
            });
        }
    }
}
