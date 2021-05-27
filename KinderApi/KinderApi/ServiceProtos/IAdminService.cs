using System.Collections.Generic;
using System.Threading.Tasks;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface IAdminService
    {
         Task<List<User>> GetBannedUsers();
    }
}