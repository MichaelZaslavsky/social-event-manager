using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using SocialEventManager.Tests.Common.DependencyInjection;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures;

public sealed class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    required public IConfiguration Configuration { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            config.AddConfiguration(Configuration);
        });

        builder.ConfigureTestServices(services => services.RegisterFakes());
    }
}
