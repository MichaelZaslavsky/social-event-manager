using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SocialEventManager.Shared.Configurations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.DependencyInjection;

public static class AuthenticationServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
    {
        JwtConfiguration jwtConfiguration = config.GetSection(ConfigurationConstants.Jwt).Get<JwtConfiguration>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new()
            {
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key)),
                ClockSkew = TimeSpan.Zero,
            };
        });

        return services;
    }
}
