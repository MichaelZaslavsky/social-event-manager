using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using SocialEventManager.API.Utilities.Extensions;
using SocialEventManager.Infrastructure.Migrations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API
{
    public static class Program
    {
        private static readonly string EnvironmentName;
        private static readonly IConfiguration Configuration;

        static Program()
        {
            EnvironmentName = Environment.GetEnvironmentVariable(ApiConstants.AspNetCoreEnvironment);

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ApiConstants.AppSettingsJson, optional: false, reloadOnChange: true)
                .AddJsonFile($"{ApiConstants.AppSettings}.{EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static void Main(string[] args)
        {
            try
            {
                IHost host = CreateHostBuilder(args).Build();
                Log.Information(ApiConstants.StartingHost);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ApiConstants.HostTerminatedUnexpectedly);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Task.Run(() => new DbMigrations(Configuration)
                .Migrate(EnvironmentName)).GetAwaiter().GetResult();

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options => options.AddServerHeader = false)
                        .UseStartup<Startup>()
                        .UseSerilog((provider, _, loggerConfig) => loggerConfig.WithSerilogLogger(provider, Configuration));
                });
        }
    }
}
