using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.API.HealthChecks
{
public static class HealthCheckEndpoints
{
    public static void MapHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks(ApiPathConstants.HealthReady, new HealthCheckOptions
        {
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            },
            ResponseWriter = HealthCheckHelpers.WriteHealthCheckReadyResponse,
            Predicate = (check) => check.Tags.Contains(ApiPathConstants.Ready),
            AllowCachingResponses = false,
        });

        endpoints.MapHealthChecks(ApiPathConstants.HealthLive, new HealthCheckOptions
        {
            Predicate = (check) => !check.Tags.Contains(ApiPathConstants.Ready),
            ResponseWriter = HealthCheckHelpers.WriteHealthCheckLiveResponse,
            AllowCachingResponses = false,
        });
    }
}
}
