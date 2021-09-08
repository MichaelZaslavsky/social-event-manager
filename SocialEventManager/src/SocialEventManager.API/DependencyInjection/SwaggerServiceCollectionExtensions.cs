using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SocialEventManager.Shared.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SocialEventManager.API.DependencyInjection
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            IApiVersionDescriptionProvider apiVersionDescriptionProvider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
            {
                foreach (ApiVersionDescription description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(ApiConstants.SocialEventManagerApi + description.GroupName, BuildOpenApiInfo(description));
                }

                options.AddSecurity();

                options.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    ApiVersionModel actionApiVersionModel = apiDescription.ActionDescriptor.GetApiVersionModel(
                        ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                    return actionApiVersionModel == null || (actionApiVersionModel.DeclaredApiVersions.Count > 0
                        ? actionApiVersionModel.DeclaredApiVersions.Any(version => $"{ApiConstants.SocialEventManagerApi}v{version}" == documentName)
                        : actionApiVersionModel.ImplementedApiVersions.Any(version => $"{ApiConstants.SocialEventManagerApi}v{version}" == documentName));
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlCommentsFullPath);
            });

            return services;
        }

        #region Private Methods

        private static OpenApiInfo BuildOpenApiInfo(ApiVersionDescription description)
        {
            return new OpenApiInfo()
            {
                Title = ApiConstants.SocialEventManagerApiTitle,
                Version = description.ApiVersion.ToString(),
                Description = ApiConstants.SocialEventManagerApiDescription,
                Contact = new OpenApiContact()
                {
                    Email = ContactConstants.Email,
                    Name = ContactConstants.Name,
                    Url = new Uri(ContactConstants.Url),
                },
                License = new OpenApiLicense()
                {
                    Name = ApiConstants.License,
                    Url = new Uri(ApiConstants.LicenseUrl),
                },
            };
        }

        private static void AddSecurity(this SwaggerGenOptions options)
        {
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
        }

        #endregion Private Methods
    }
}
