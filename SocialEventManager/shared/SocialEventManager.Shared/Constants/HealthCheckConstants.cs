using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SocialEventManager.Shared.Constants
{
    public static class HealthCheckConstants
    {
        public static Dictionary<HealthStatus, int> HealthStatusCodes =>
            new()
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            };
    }
}
