using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Hubs
{
    public class ChatMessage
    {
        public string User { get; set; }

        public string Message { get; set; }
    }

    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
    }

    public class PostHub : Hub
    {
        public async Task UserPosted(string userId)
        {
                await Clients.All.SendAsync("UserPosted", userId);
        }
    }
}
