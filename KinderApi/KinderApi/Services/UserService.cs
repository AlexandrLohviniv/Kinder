using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.EntityFrameworkCore;

namespace KinderApi.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext context;

        public UserService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> allUsers = await context.Users.ToListAsync();

            return allUsers;
        }

        public async Task<Image> GetMainPhotoByUser(int userId)
        {
            var photoQuery = context.Image.AsQueryable();
            var photo = await photoQuery.FirstOrDefaultAsync(p => p.IsMain && p.Userid == userId);

            return photo;
        }

        public async Task<User> GetUserById(int userId)
        {
            var query = context.Users.Include(u => u.Images).AsQueryable();
            var user = await query.FirstOrDefaultAsync(u => u.Id == userId);

           return user;
        }

        public async Task<Image> GetUserPhotoByPhotoId(int userId, int photoId)
        {
            var photoQuery = context.Image.AsQueryable();
            var photo = await photoQuery.FirstOrDefaultAsync(p => p.id == photoId && p.Userid == userId);

            return photo;
        }
    }
}