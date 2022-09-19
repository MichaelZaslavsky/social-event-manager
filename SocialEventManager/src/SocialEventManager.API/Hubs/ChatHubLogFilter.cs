using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Serilog;

namespace SocialEventManager.API.Hubs;

[ExcludeFromCodeCoverage]
public sealed class ChatHubLogFilter : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        string message = JsonConvert.SerializeObject(invocationContext.HubMethodArguments);
        Log.Information("Conversation message: '{message}'", message);

        return await next(invocationContext);
    }
}
