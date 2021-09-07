using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiConstants.FirstVersion, new OpenApiInfo
                {
                    Version = ApiConstants.FirstVersion,
                    Title = ApiConstants.SocialEventManagerApi,
                });

                options.AddSecurityDefinition(AuthConstants.BasicAuth, new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = AuthConstants.AuthScheme,
                    Description = AuthConstants.SwaggerAuthenticationDescription,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = AuthConstants.BasicAuth,
                            },
                        },
                        new List<string>()
                    },
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlCommentsFullPath);
            });

            return services;
        }
    }
}
