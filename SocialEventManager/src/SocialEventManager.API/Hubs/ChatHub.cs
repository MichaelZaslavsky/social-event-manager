using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.Hubs;

[ExcludeFromCodeCoverage]
public sealed class ChatHub : Hub
{
    public async Task SendMessage(Guid userId, string message)
    {
        Log.Information("User '{userId}' receive message '{message}'", userId, message);
        await Clients.All.SendAsync(ApiConstants.ReceiveMessage, userId, message);
    }
}
