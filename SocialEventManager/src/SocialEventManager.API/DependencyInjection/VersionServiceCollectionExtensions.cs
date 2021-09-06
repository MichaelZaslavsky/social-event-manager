using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class VersionServiceCollectionExtensions
    {
        public static IServiceCollection AddSupportedApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader(ApiConstants.ApiVersion),
                    new MediaTypeApiVersionReader(ApiConstants.ApiVersion),
                    new QueryStringApiVersionReader(ApiConstants.Ver, ApiConstants.Version));
            });

            return services;
        }
    }
}
