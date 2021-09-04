using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.API.HealthChecks
{
    public static class HealthCheckEndpoints
    {
        public static void MapHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks(ApiPathConstants.HealthReady, new HealthCheckOptions()
            {
                ResultStatusCodes = HealthCheckConstants.HealthStatusCodes,
                ResponseWriter = HealthCheckHelpers.WriteHealthCheckReadyResponse,
                Predicate = (check) => check.Tags.Contains(ApiPathConstants.Ready),
                AllowCachingResponses = false,
            });

            endpoints.MapHealthChecks(ApiPathConstants.HealthLive, new HealthCheckOptions()
            {
                ResultStatusCodes = HealthCheckConstants.HealthStatusCodes,
                Predicate = (check) => !check.Tags.Contains(ApiPathConstants.Ready),
                ResponseWriter = HealthCheckHelpers.WriteHealthCheckLiveResponse,
                AllowCachingResponses = false,
            });
        }
    }
}
