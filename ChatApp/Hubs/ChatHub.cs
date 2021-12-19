using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public void SendMessage(ChatMessage message)
        {
            Clients.All.SendAsync("ChatMessageRecieved", message);
        }
        
        public void SignedUser(UserModel user)
        {
            var userInfo = new SignedUserModel
            {
                Login = user.Login
            };
            Clients.All.SendAsync("SignedUser", userInfo);
        }
    }
}