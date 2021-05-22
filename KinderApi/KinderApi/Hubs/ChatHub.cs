using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace KinderApi.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (Context?.User?.Identity?.Name != null)
                await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);


            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context?.User?.Identity?.Name != null)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendToUser(int toId,int fromId, string message)
        {
            await Clients.Group(toId.ToString()).SendAsync("RecievePrivateMessage", fromId, message);
            await Clients.Caller.SendAsync("RecieveOwnMessage",message);
        }
    }
}