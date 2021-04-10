using System.Threading.Tasks;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface ILoginService
    {
         Task<bool> LoginUser(string mail, string password);
         Task<bool> RegisterUSer(User user);
    }
}