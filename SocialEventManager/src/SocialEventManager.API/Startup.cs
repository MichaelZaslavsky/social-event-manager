using System;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialEventManager.API.DependencyInjection;
using SocialEventManager.API.Utilities.Handlers;
using SocialEventManager.BLL.Services;
using SocialEventManager.DLL.Repositories;
using SocialEventManager.Infrastructure.Attributes;
using SocialEventManager.Infrastructure.Filters;
using SocialEventManager.Infrastructure.Filters.BackgroundJobs;
using SocialEventManager.Infrastructure.Loggers;
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
            services.AddCors(options => options.AddPolicy(ApiConstants.AllowAll, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddSwagger()
                .AddSqlServer(Configuration)
                .AddHangfire(Configuration)
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ValidationFilterAttribute>()
                .AddSingleton<IScopeInformation, ScopeInformation>();

            // Temp code - for test purposes
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUsersRepository, UsersRepository>();

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
            app.UseHangfireDashboard();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint($"/{ApiConstants.Swagger}/{ApiConstants.FirstVersion}/{ApiConstants.Swagger}.json", ApiConstants.SocialEventManagerApi));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(ApiConstants.AllowAll);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => await context.Response.WriteAsync("Success"));
                endpoints.MapControllers();
            });
        }
    }
}
