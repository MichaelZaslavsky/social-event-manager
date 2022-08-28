using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using SocialEventManager.API.DependencyInjection;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Tests.Common.Constants;
using SocialEventManager.Tests.IntegrationTests.Infrastructure;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace SocialEventManager.Tests;

public class Startup
{
    public IConfiguration Configuration { get; set; } = null!;

    public void ConfigureServices(IServiceCollection services)
    {
        const string environmentName = TestConstants.TestingEnvironmentName;

        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(ApiConstants.AppSettingsJson, optional: false, reloadOnChange: true)
            .AddJsonFile($"{ApiConstants.AppSettings}.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .AddUserSecrets<Program>()
            .Build();

        services.AddRazorPages();

        DiagnosticListener listener = new("Microsoft.AspNetCore");
        services.AddSingleton(listener)
            .AddSingleton<DiagnosticSource>(listener);

        services.Configure(Configuration)
            .AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug))
            .AddSingleton<IInMemoryDatabase, InMemoryDatabase>(_ =>
                new InMemoryDatabase(Configuration.GetConnectionString(DbConstants.SocialEventManagerTest)))
            .AddSingleton(Configuration)
            .RegisterDependencies()
            .AddRedisClients(Configuration);

        services.AddSingleton(sp =>
        {
            IInMemoryDatabase provider = sp.GetRequiredService<IInMemoryDatabase>();
            Mock<IDbSession> mock = provider.GetMockDbSession();

            return mock.Object;
        });
    }

    public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor) =>
        loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));
}
