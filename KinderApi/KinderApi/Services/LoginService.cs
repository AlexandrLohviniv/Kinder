using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;

namespace KinderApi.Services
{
    public class LoginService: ILoginService
    {
        private readonly DatabaseContext context;

        public LoginService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<bool> LoginUser(string mail, string password)
        {
            if(mail == "kek@test.com" && password=="1234")
                return true;
            
            return false;
        }

        public async Task<bool> RegisterUSer(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}