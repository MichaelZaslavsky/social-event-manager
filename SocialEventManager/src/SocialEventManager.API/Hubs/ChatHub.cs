using Microsoft.AspNetCore.SignalR;
using Serilog;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Guid userId, string message)
        {
            Log.Information($"User '{userId}' receive message '{message}'");
            await Clients.All.SendAsync(ApiConstants.ReceiveMessage, userId, message);
        }
    }
}
