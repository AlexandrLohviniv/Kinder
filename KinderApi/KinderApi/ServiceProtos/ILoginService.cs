using System.Threading.Tasks;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface ILoginService
    {
         Task<User> LoginUser(string mail, string password);
         Task<User> RegisterUSer(User user);
    }
}