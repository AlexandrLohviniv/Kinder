using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.EntityFrameworkCore;

namespace KinderApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly DatabaseContext context;

        public AdminService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<User>> GetBannedUsers()
        {
            var users = context.BannedUsers.Include(x => x.User);
            if (users == null)
                return new List<User>();

            List<User> usersToReturn = await users.Select(u => u.User).ToListAsync();

            return usersToReturn;
        }


    }
}