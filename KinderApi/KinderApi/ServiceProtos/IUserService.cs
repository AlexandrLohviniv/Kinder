using System.Collections.Generic;
using System.Threading.Tasks;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface IUserService
    {
         Task<List<User>> GetAllUsers();
    }
}