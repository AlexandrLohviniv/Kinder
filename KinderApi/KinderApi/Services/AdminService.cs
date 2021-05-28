using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinderApi.helper;
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

        public async Task<PagedList<User>> GetBannedUsers(PaginationParams userParams)
        {
            var usersQuery = context.BannedUsers.Include(x => x.User);

            var users = usersQuery.Select(u => u.User);

            PagedList<User> result = await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);

            return result;
        }
    }
}