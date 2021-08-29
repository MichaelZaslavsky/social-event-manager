using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SocialEventManager.Shared.Helpers
{
    public static class HealthCheckHelpers
    {
        public static Task WriteHealthCheckReadyResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("OverallStatus", result.Status.ToString()),
                new JProperty("TotalChecksDuration", result.TotalDuration.TotalSeconds.ToString("0:0.00")),
                new JProperty("DependencyHealthChecks", new JObject(result.Entries.Select(item =>
                    new JProperty(item.Key, new JObject(
                            new JProperty("Status", item.Value.Status.ToString()),
                            new JProperty("Duration", item.Value.Duration.TotalSeconds.ToString("0:0.00")),
                            new JProperty("Exception", item.Value.Exception?.Message),
                            new JProperty("Data", new JObject(item.Value.Data.Select(data =>
                                new JProperty(data.Key, data.Value))))))))));

            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}
