using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Hubs
{
    public class PostHub : Hub<IPostHub>
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendMessage(message);
        }
    }
}
