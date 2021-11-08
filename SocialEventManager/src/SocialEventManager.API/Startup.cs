using System;
using System.Linq;
using AspNetCoreRateLimit;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialEventManager.API.Authentication;
using SocialEventManager.API.Configurations;
using SocialEventManager.API.DependencyInjection;
using SocialEventManager.API.HealthChecks;
using SocialEventManager.API.Hubs;
using SocialEventManager.API.Utilities.Handlers;
using SocialEventManager.Infrastructure.Attributes;
using SocialEventManager.Infrastructure.Filters;
using SocialEventManager.Infrastructure.Filters.BackgroundJobs;
using SocialEventManager.Infrastructure.Middleware;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BasicAuthenticationConfiguration>(Configuration.GetSection(AuthConstants.BasicAuthentication))
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(TrackActionPerformanceFilter));
                    options.ConfigureGlobalResponseTypeAttributes();
                    options.ReturnHttpNotAcceptable = true;
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                });

            services.AddCors(options => options.AddPolicy(ApiConstants.AllowAll, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()))
                .AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VV")
                .AddAuthentication(AuthConstants.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthConstants.AuthenticationScheme, null);

            services.AddSupportedApiVersioning()
                .AddSwagger()
                .AddOptions()
                .AddMemoryCache()
                .AddRateLimiting(Configuration)
                .AddSqlServer(Configuration)
                .AddHangfire(Configuration)
                .RegisterDi()
                .AddIdentityConfigurations()
                .AddRedisClients(Configuration)
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ValidationFilterAttribute>()
                .AddHealthChecks(Configuration)
                .AddResponseCompression(options => options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { MediaTypeConstants.ApplicationOctetStream }));

            services.AddSignalR().AddHubOptions<ChatHub>(options => options.AddFilter<ChatHubLogFilter>());
            GlobalJobFilters.Filters.Add(new HangfireElectStateEventsLogAttribute());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseApiExceptionHandler(options =>
            {
                options.AddResponseDetails = ErrorResponseHandler.UpdateApiErrorResponse;
                options.DetermineLogLevel = ErrorResponseHandler.DetermineLogLevel;
            })
            .UseResponseCompression()

            // TODO: Currently, the Hangfire dashboard is opened to all users. Need to implement an authorization scenario.
            // A helpful link for implementing it:
            // https://sahansera.dev/securing-hangfire-dashboard-with-endpoint-routing-auth-policy-aspnetcore/
            .UseHangfireDashboard(ApiPathConstants.Hangfire, new DashboardOptions
            {
                Authorization = new[] { new AllowAllConnectionsFilter() },
                IgnoreAntiforgeryToken = true,
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
           }
            else
            {
                app.UseHsts();
            }

            app.UseSecurityHeaders(SecurityPolicyConfigurations.GetPermissionPolicies())
                .UseHttpsRedirection()
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (ApiVersionDescription description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/{ApiConstants.Swagger}/{ApiConstants.SocialEventManagerApi}{description.GroupName}/{ApiConstants.Swagger}.json",
                            description.GroupName.ToUpperInvariant());
                    }
                })
                .UseRouting()
                .UseCors(ApiConstants.AllowAll)
                .UseIpRateLimiting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", async context => await context.Response.WriteAsync("Success"));
                    endpoints.MapHealthChecks();
                    endpoints.MapControllers();
                    endpoints.MapHub<ChatHub>(ApiPathConstants.ChatHub);
                });
        }

        // A temporary solution for allowing the HangFire dashboard to work with Docker.
        // At a later stage this class should be replaced with a one that will authorize the user who wants to use the Hangfire dashboard.
        public class AllowAllConnectionsFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                return true;
            }
        }
    }
}
