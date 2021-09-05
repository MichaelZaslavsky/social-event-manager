using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Serilog;

namespace SocialEventManager.API.Hubs
{
    public class ChatHubLogFilter : IHubFilter
    {
        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            string message = JsonConvert.SerializeObject(invocationContext.HubMethodArguments);
            Log.Information($"Conversation message: '{message}'");

            return await next(invocationContext);
        }
    }
}
