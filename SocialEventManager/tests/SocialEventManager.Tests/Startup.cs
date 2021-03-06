using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using SocialEventManager.API.DependencyInjection;
using SocialEventManager.BLL.Services.DependencyInjection;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace SocialEventManager.Tests;

public class Startup
{
    public IConfiguration Configuration { get; set; } = null!;

    public void ConfigureServices(IServiceCollection services)
    {
        Configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets<Startup>()
            .Build();

        services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug))
            .AddSingleton<IInMemoryDatabase, InMemoryDatabase>(_ =>
                new InMemoryDatabase(Configuration.GetConnectionString(DbConstants.SocialEventManagerTest)))
            .AddSingleton(Configuration)
            .RegisterServices()
            .AddRedisClients(Configuration);

        services.AddScoped(sp =>
        {
            IInMemoryDatabase provider = sp.GetRequiredService<IInMemoryDatabase>();
            Mock<IDbSession> mock = provider.GetMockDbSession();

            return mock.Object;
        });
    }

    public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor) =>
        loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));
}
