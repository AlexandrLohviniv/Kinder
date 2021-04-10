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
    }
}