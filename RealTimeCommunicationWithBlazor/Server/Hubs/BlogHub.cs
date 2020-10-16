using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeCommunicationWithBlazor.Server.Hubs
{
    public class BlogHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("newclient", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async Task SendMessage()
        {
            await Clients.All.SendAsync("NewBlogAddition");
        }

    }
}
