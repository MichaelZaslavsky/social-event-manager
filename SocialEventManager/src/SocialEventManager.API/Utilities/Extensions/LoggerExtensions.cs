using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using SocialEventManager.API.Utilities.Logging;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.Utilities.Extensions;

public static class LoggerExtensions
{
    public static void WithSerilogLogger(this LoggerConfiguration loggerConfig, IServiceProvider provider, IConfiguration config)
    {
        string seqServerUrl = config[ApiConstants.SeqServerUrlSettingPath];
        string? assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;

        loggerConfig.ReadFrom.Configuration(config)
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty(ApiConstants.Assembly, assemblyName ?? string.Empty)
            .Enrich.WithAspnetcoreHttpcontext(provider, GetContextInfo)
            .WriteTo.Seq(seqServerUrl);
    }

    #region Private Methods

    private static ContextInformation? GetContextInfo(IHttpContextAccessor accessor)
    {
        HttpContext? context = accessor?.HttpContext;

        return context is null
            ? null
            : new()
            {
                RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString(),
                Host = context.Request.Host.ToString(),
                Method = context.Request.Method,
                Protocol = context.Request.Protocol,
                UserInfo = GetUserInfo(context.User),
            };
    }

    private static UserInformation? GetUserInfo(ClaimsPrincipal principal)
    {
        IIdentity? user = principal.Identity;

        if (user?.IsAuthenticated != true)
        {
            return null;
        }

        List<string> excludedClaims = new() { "nbf", "exp", "auth_time", "amr", "sub", "at_hash", "s_hash", "sid", "name", "preferred_username" };
        const string userNameClaimType = "name";

        UserInformation userInfo = new()
        {
            UserId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value,
            UserName = principal.Claims.FirstOrDefault(c => c.Type == userNameClaimType)?.Value,
            UserClaims = new Dictionary<string, IList<string>>(),
        };

        foreach (string claimType in principal.Claims
            .Where(c => excludedClaims.All(ec => ec != c.Type))
            .Select(c => c.Type)
            .Distinct())
        {
            userInfo.UserClaims[claimType] = principal.Claims
                .Where(c => c.Type == claimType)
                .Select(c => c.Value)
                .ToList();
        }

        return userInfo;
    }

    #endregion Private Methods
}
