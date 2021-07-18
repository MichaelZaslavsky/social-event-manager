using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using SocialEventManager.Tests.Common.DependencyInjection;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures
{
    public class ApiWebApplicationFactory : WebApplicationFactory<API.Startup>
    {
        public IConfiguration Configuration { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                  .Build();

                config.AddConfiguration(Configuration);
            });

            builder.ConfigureTestServices(services => services.RegisterRepositories());
        }
    }
}
