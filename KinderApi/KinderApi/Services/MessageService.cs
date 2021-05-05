using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;

namespace KinderApi.Services
{
    public class MessageService : IMessageService
    {
        private readonly DatabaseContext context;
        private readonly IUserService userService;

        public MessageService(DatabaseContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<List<Message>> GetMesageThread(int fromId, int toId)
        {
            User fromUser = await userService.GetUserById(fromId);
            User toUser = await userService.GetUserById(toId);

            if (fromUser == null || toUser == null)
                return null;

            List<Message> messages = context.Messages.Where(m => (m.SenderId == fromId && m.ReceiverId == toId) ||
                                                                (m.SenderId == toId && m.ReceiverId == fromId))
                                                     .OrderBy(m => m.sendingTime)
                                                     .ToList();
            return messages;
        }

        public async Task SendMessage(int fromId, int toId, string message)
        {
            User fromUser = await userService.GetUserById(fromId);
            User toUser = await userService.GetUserById(toId);

            Message newMessage = new Message()
            {
                Sender = fromUser,
                Receiver = toUser,
                Text = message,
                sendingTime = DateTime.Now,
                isRead = false
            };

            await context.Messages.AddAsync(newMessage);
            await context.SaveChangesAsync();
        }
    }
}