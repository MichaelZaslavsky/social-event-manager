using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialEventManager.API.DependencyInjection;
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
            services.AddControllers();
            services.AddCors(options => options.AddPolicy(ApiConstants.AllowAll, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddSwagger()
                .AddSqlServer(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint($"/{ApiConstants.Swagger}/{ApiConstants.FirstVersion}/{ApiConstants.Swagger}.json", ApiConstants.SocialEventManagerApi));

            app.UseCors(ApiConstants.AllowAll);

            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/", async context => await context.Response.WriteAsync("Success")));
        }
    }
}
