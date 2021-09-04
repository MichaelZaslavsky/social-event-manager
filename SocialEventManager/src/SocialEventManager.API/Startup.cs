using System;
using AspNetCoreRateLimit;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialEventManager.API.DependencyInjection;
using SocialEventManager.API.HealthChecks;
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
            services.AddCors(options => options.AddPolicy(ApiConstants.AllowAll, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()))
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
                .AddHealthChecks(Configuration);

            services.AddControllers(config => config.Filters.Add(typeof(TrackActionPerformanceFilter)));
            GlobalJobFilters.Filters.Add(new HangfireElectStateEventsLogAttribute());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiExceptionHandler(options =>
            {
                options.AddResponseDetails = ErrorResponseHandler.UpdateApiErrorResponse;
                options.DetermineLogLevel = ErrorResponseHandler.DetermineLogLevel;
            });
            app.UseHsts();

            // TODO: Currently, the Hangfire dashboard is opened to all users. Need to implement an authorization scenario.
            // A helpful link for implementing it:
            // https://sahansera.dev/securing-hangfire-dashboard-with-endpoint-routing-auth-policy-aspnetcore/
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new AllowAllConnectionsFilter() },
                IgnoreAntiforgeryToken = true,
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint($"/{ApiConstants.Swagger}/{ApiConstants.FirstVersion}/{ApiConstants.Swagger}.json", ApiConstants.SocialEventManagerApi));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseIpRateLimiting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(ApiConstants.AllowAll);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => await context.Response.WriteAsync("Success"));
                endpoints.MapHealthChecks();
                endpoints.MapControllers();
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
