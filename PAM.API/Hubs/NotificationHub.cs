using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.API.Hubs
{
    public class NotificationHub : Hub
    {

        /// <summary>
        /// This endpoint sends notification to the users
        /// </summary>
        /// <param name="id"></param>
        public async Task Subscribe(string id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
