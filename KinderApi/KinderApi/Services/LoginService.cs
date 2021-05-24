using System.Linq;
using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.EntityFrameworkCore;


namespace KinderApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly DatabaseContext context;

        public LoginService(DatabaseContext context)
        {
            this.context = context;

        }

        public async Task<User> LoginUser(string mail, string password)
        {
            User user = await context.Users.FirstOrDefaultAsync(x => x.Email == mail && x.Password == password);

            if (user != null)
            {
                var BannedUser = await context.BannedUsers.FirstOrDefaultAsync(c => c.UserId == user.Id);
                if (BannedUser != null)
                    return null;
            }
            return user;
        }

        public async Task<User> RegisterUSer(User user)
        {
            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }
            return user;
        }
    }
}