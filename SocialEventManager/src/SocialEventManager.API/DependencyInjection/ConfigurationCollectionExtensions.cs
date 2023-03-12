using SocialEventManager.Shared.Configurations;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.API.DependencyInjection;

public static class ConfigurationCollectionExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration config)
    {
        IConfigurationSection emailConfig = GetSection(config, ConfigurationConstants.Email);
        IConfigurationSection jwtConfig = GetSection(config, ConfigurationConstants.Jwt);
        IConfigurationSection hangfireSettingsConfig = GetSection(config, ConfigurationConstants.HangfireSettings);

        ValidateSections(config, new[] { ConfigurationConstants.AppUrl, ApiConstants.Redis });
        ValidateConfigKeys(config, new[] { ApiConstants.SeqServerUrlSettingPath });
        ValidateConnectionStrings(config, new[] { DbConstants.SocialEventManager, DbConstants.SocialEventManagerHangfire });

        services.Configure<EmailConfiguration>(emailConfig);
        services.Configure<JwtConfiguration>(jwtConfig);
        services.Configure<HangfireSettingsConfiguration>(hangfireSettingsConfig);

        return services;
    }

    private static IConfigurationSection GetSection(IConfiguration config, string section)
    {
        IConfigurationSection? configSection = config.GetSection(section);
        ConfigurationHelpers.ThrowIfNull(configSection, section);

        return configSection;
    }

    private static void ValidateSections(IConfiguration config, string[] keys)
    {
        foreach (string key in keys)
        {
            IConfigurationSection? configSection = config.GetSection(key);
            ConfigurationHelpers.ThrowIfNull(configSection, key);
        }
    }

    private static void ValidateConfigKeys(IConfiguration config, string[] keys)
    {
        foreach (string key in keys)
        {
            string? value = config[key];
            ConfigurationHelpers.ThrowIfNull(value, key);
        }
    }

    private static void ValidateConnectionStrings(IConfiguration config, string[] names)
    {
        foreach (string name in names)
        {
            string? connection = config.GetConnectionString(name);
            ConfigurationHelpers.ThrowIfNull(connection, name);
        }
    }
}
