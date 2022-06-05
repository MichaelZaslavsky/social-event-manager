using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Helpers;

public static class HealthCheckHelpers
{
    private const string SecondsPattern = "0:0.00";

    public static Task WriteHealthCheckReadyResponse(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = MediaTypeConstants.ApplicationJson;

        JObject json = new(
            new JProperty("OverallStatus", result.Status.ToString()),
            new JProperty("TotalChecksDuration", result.TotalDuration.TotalSeconds.ToString(SecondsPattern)),
            new JProperty("DependencyHealthChecks", new JObject(result.Entries.Select(item =>
                new JProperty(item.Key, new JObject(
                        new JProperty("Status", item.Value.Status.ToString()),
                        new JProperty("Duration", item.Value.Duration.TotalSeconds.ToString(SecondsPattern)),
                        new JProperty("Exception", item.Value.Exception?.Message),
                        new JProperty("Data", new JObject(item.Value.Data.Select(data =>
                            new JProperty(data.Key, data.Value))))))))));

        return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
    }

    public static Task WriteHealthCheckLiveResponse(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = MediaTypeConstants.ApplicationJson;

        JObject json = new(
            new JProperty("OverallStatus", result.Status.ToString()),
            new JProperty("TotalChecksDuration", result.TotalDuration.TotalSeconds.ToString(SecondsPattern)));

        return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
    }
}
