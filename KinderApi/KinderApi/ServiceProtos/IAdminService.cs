using System.Collections.Generic;
using System.Threading.Tasks;
using KinderApi.helper;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface IAdminService
    {
         Task<PagedList<User>> GetBannedUsers(PaginationParams userParams);
    }
}