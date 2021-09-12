using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using morekaluAPI.Models;
using morekaluAPI.Controllers;

namespace SignalRChat.Hubs
{ 
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, Message message)
        {
            
            ChatReader reader = new ChatReader("Database/db_chat.json");
            reader.addRecord(message);

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}