using System.Threading.Tasks;

namespace KinderApi.ServiceProtos
{
    public interface ILoginService
    {
         Task<bool> LoginUser(string mail, string password);
    }
}