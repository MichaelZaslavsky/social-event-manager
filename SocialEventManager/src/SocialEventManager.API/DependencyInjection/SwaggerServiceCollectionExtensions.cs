using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetEscapades.AspNetCore.SecurityHeaders;
using SocialEventManager.Shared.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

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

            string xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            options.IncludeXmlComments(xmlCommentsFullPath);
        })
        .SecureSwagger();

        return services;
    }

    #region Private Methods

    /// <summary>
    /// Fix &lt;script&gt; tag and inline &lt;script&gt; tags used by Swagger by modifying the content dynamically for each HTTP request
    /// Read more here - https://purple.telstra.com/blog/locking-down-csp-with-aspnet-core-and-swashbuckle.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    private static void SecureSwagger(this IServiceCollection services)
    {
        services.AddHttpContextAccessor()
        .AddOptions<SwaggerUIOptions>()
        .Configure<IHttpContextAccessor>((options, httpContextAccessor) =>
        {
            // Take a reference of the original Stream factory which reads from Swashbuckle's embedded resources
            Func<Stream> originalIndexStreamFactory = options.IndexStream;

            // Override the Stream factory
            options.IndexStream = () =>
            {
                // Read the original index.html file
                using Stream originalStream = originalIndexStreamFactory();
                using StreamReader originalStreamReader = new(originalStream);
                string originalIndexHtmlContents = originalStreamReader.ReadToEnd();

                // Get the request-specific nonce generated by NetEscapades.AspNetCore.SecurityHeaders
                string requestSpecificNonce = httpContextAccessor.HttpContext.GetNonce();

                // Replace inline `<script>` and `<style>` tags by adding a `nonce` attribute to them
                string nonceEnabledIndexHtmlContents = originalIndexHtmlContents
                                                       .Replace("<script>", $"<script nonce=\"{requestSpecificNonce}\">", StringComparison.OrdinalIgnoreCase)
                                                       .Replace("<style>", $"<style nonce=\"{requestSpecificNonce}\">", StringComparison.OrdinalIgnoreCase);

                // Return a new Stream that contains our modified contents
                return new MemoryStream(Encoding.UTF8.GetBytes(nonceEnabledIndexHtmlContents));
            };
        });
    }

    private static OpenApiInfo BuildOpenApiInfo(ApiVersionDescription description)
    {
        return new OpenApiInfo
        {
            Title = ApiConstants.SocialEventManagerApiTitle,
            Version = description.ApiVersion.ToString(),
            Description = ApiConstants.SocialEventManagerApiDescription,
            Contact = new OpenApiContact
            {
                Email = ContactConstants.Email,
                Name = ContactConstants.Name,
                Url = new Uri(ContactConstants.Url),
            },
            License = new OpenApiLicense
            {
                Name = ApiConstants.License,
                Url = new Uri(ApiConstants.LicenseUrl),
            },
        };
    }

    private static void AddSecurity(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(AuthConstants.BasicAuth, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = AuthConstants.Scheme,
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
