using System.Linq;
using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.EntityFrameworkCore;

namespace KinderApi.Services
{
    public class LoginService: ILoginService
    {
        private readonly DatabaseContext context;

        public LoginService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<User> LoginUser(string mail, string password)
        {
            User user = await context.Users.FirstOrDefaultAsync(x => x.Email == mail && x.Password == password);

            return user;
        }

        public async Task<bool> RegisterUSer(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}