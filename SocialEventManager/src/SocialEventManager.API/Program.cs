using AspNetCoreRateLimit;
using Hangfire;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using SocialEventManager.API.Authentication;
using SocialEventManager.API.Configurations;
using SocialEventManager.API.DependencyInjection;
using SocialEventManager.API.HealthChecks;
using SocialEventManager.API.Hubs;
using SocialEventManager.API.Utilities.Extensions;
using SocialEventManager.API.Utilities.Handlers;
using SocialEventManager.DAL.Migrations;
using SocialEventManager.Infrastructure.Attributes;
using SocialEventManager.Infrastructure.Filters;
using SocialEventManager.Infrastructure.Filters.BackgroundJobs;
using SocialEventManager.Infrastructure.Middleware;
using SocialEventManager.Shared.Constants;

const int FiftyMb = 52428800;

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    string environmentName = Environment.GetEnvironmentVariable(ApiConstants.AspNetCoreEnvironment)!;
    ArgumentNullException.ThrowIfNull(environmentName);

    builder.WebHost
        .ConfigureKestrel(options =>
        {
            options.AddServerHeader = false;
            options.Limits.MaxConcurrentConnections = 100;
            options.Limits.MaxConcurrentUpgradedConnections = 100;
            options.Limits.MaxRequestBodySize = FiftyMb;
        })
        .UseSerilog((provider, _, loggerConfig) => loggerConfig.WithSerilogLogger(provider, builder.Configuration));

    builder.Host.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
    });

    builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(ApiConstants.AppSettingsJson, optional: false, reloadOnChange: true)
        .AddJsonFile($"{ApiConstants.AppSettings}.{environmentName}.json", optional: true)
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>()
        .Build();

    ConfigureServices(builder.Services, builder.Configuration);

    WebApplication app = builder.Build();
    IApiVersionDescriptionProvider apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    ConfigureApplication(app, apiVersionDescriptionProvider);

    Log.Information(ApiConstants.StartingHost);
    Task.Run(() => new DbMigrations(builder.Configuration).Migrate(environmentName).GetAwaiter().GetResult());

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, ApiConstants.HostTerminatedUnexpectedly);
}
finally
{
    Log.CloseAndFlush();
}

static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
    services.Configure(config)
        .AddControllers(options =>
        {
            options.Filters.Add(typeof(TrackActionPerformanceFilter));
            options.ConfigureGlobalResponseTypeAttributes();
            options.ReturnHttpNotAcceptable = true;
            options.OutputFormatters.RemoveType<StringOutputFormatter>();
            options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });

    services.AddCors(options => options.AddPolicy(
        name: ApiConstants.AllowOrigin,
        builder =>
        {
            builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }))
        .AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VV")
        .AddAuthentication(AuthConstants.AuthenticationScheme)
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(AuthConstants.AuthenticationScheme, null);

    services.AddSupportedApiVersioning()
        .AddSwagger()
        .AddOptions()
        .AddMemoryCache()
        .AddRateLimiting(config)
        .AddSqlServer(config)
        .AddHangfire(config)
        .RegisterDependencies()
        .AddRedisClients(config)
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddScoped<ValidationFilterAttribute>()
        .AddHealthChecks(config)
        .AddResponseCompression(options => options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { MediaTypeConstants.ApplicationOctetStream }));

    services.AddSignalR().AddHubOptions<ChatHub>(options => options.AddFilter<ChatHubLogFilter>());
    GlobalJobFilters.Filters.Add(new HangfireElectStateEventsLogAttribute());
}

static void ConfigureApplication(WebApplication app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
{
    // TODO: Currently, the Hangfire dashboard is opened to all users. Need to implement an authorization scenario.
    // A helpful link for implementing it:
    // https://sahansera.dev/securing-hangfire-dashboard-with-endpoint-routing-auth-policy-aspnetcore/
    app.UseHangfireDashboard(ApiPathConstants.Hangfire, new DashboardOptions
    {
        Authorization = new[] { new AllowAllConnectionsFilter() },
        IgnoreAntiforgeryToken = true,
    });

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseApiExceptionHandler(options =>
        {
            options.AddResponseDetails = ErrorResponseHandler.UpdateApiErrorResponse;
            options.DetermineLogLevel = ErrorResponseHandler.DetermineLogLevel;
        })
        .UseResponseCompression().UseSecurityHeaders(SecurityPolicyConfigurations.GetPermissionPolicies())
        .UseHttpsRedirection()
        .UseSwagger()
        .UseSwaggerUI(options =>
        {
            foreach (string groupName in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(description => description.GroupName))
            {
                options.SwaggerEndpoint(
                    $"/{ApiConstants.Swagger}/{ApiConstants.SocialEventManagerApi}{groupName}/{ApiConstants.Swagger}.json",
                    groupName.ToUpperInvariant());
            }
        })
        .UseRouting()
        .UseCors(ApiConstants.AllowOrigin)
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

// This is used as a workaround for letting WebApplicationFactory access the Program file.
// For more info, read https://stackoverflow.com/questions/69058176/how-to-use-webapplicationfactory-in-net6-without-speakable-entry-point.
#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program
{
}
#pragma warning restore CA1050 // Declare types in namespaces
