using System.Collections.Generic;
using System.Threading.Tasks;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface ILikeService
    {
         Task SendLike(int fromId, int toId);
         Task GetbackLike(int fromId, int toId);
         Task<List<User>> PairsForUser(int userId);
    }
}