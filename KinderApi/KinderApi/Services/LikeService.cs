using System;
using System.Linq;
using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;

namespace KinderApi.Services
{
    public class LikeService : ILikeService
    {
        private readonly DatabaseContext context;
        private readonly IUserService userService;

        public LikeService(DatabaseContext context, IUserService userService)
        {
            this.userService = userService;
            this.context = context;
        }

        public async Task GetbackLike(int fromId, int toId)
        {
            var likes = context.Likes.AsQueryable();
            Like like = likes.FirstOrDefault(l => l.SenderId == fromId && l.ReceiverId == toId);
            if (like == null)
                return;

            context.Likes.Remove(like);
            await context.SaveChangesAsync();
        }

        public async Task SendLike(int fromId, int toId)
        {
            User fromUser = await userService.GetUserById(fromId);
            User toUser = await userService.GetUserById(toId);

            Like like = new Like()
            {
                Receiver = toUser,
                Sender = fromUser,
                sendingTime = DateTime.Now
            };

            await context.Likes.AddAsync(like);
            await context.SaveChangesAsync();
        }
    }
}