using System.Threading.Tasks;
using KinderApi.ServiceProtos;

namespace KinderApi.Services
{
    public class LoginService: ILoginService
    {
        public async Task<bool> LoginUser(string mail, string password)
        {
            if(mail == "kek@test.com" && password=="1234")
                return true;
            
            return false;
        }
    }
}