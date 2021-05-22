using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KinderMobile
{
    public class ChatService
    {
        private static ChatService m_instance;

        public static ChatService Instance 
        {
            get 
            {
                return m_instance;
            }
        }

        public static void InitializaClient(string Token) 
        {
            m_instance = new ChatService(Token);
        }

        private readonly HubConnection hubConnection;



        public ChatService(string Token)
        {
            hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.1.104:5000/ChatHub", options => 
            {
                options.AccessTokenProvider = () => Task.FromResult(Token);
            }).WithAutomaticReconnect().Build();
        }

        public async Task Connect() 
        {
            await hubConnection.StartAsync();
        }


        public async Task Disconnect() 
        {
            await hubConnection.StopAsync();
        }

        public async Task SendPrivateMessage(int userId, int fromId, string message)
        {
            try
            {
                await hubConnection.SendAsync("SendToUser", userId, fromId, message);
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        public void RecievePrivateMessage(Action<int, string> GetMessageAndUser, Action<string> GetMyMessage)
        {
            hubConnection.On("RecievePrivateMessage", GetMessageAndUser);
            hubConnection.On("RecieveOwnMessage", GetMyMessage);
        }

    }
}
