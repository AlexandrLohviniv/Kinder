using System.Threading.Tasks;

namespace KinderApi.ServiceProtos
{
    public interface ILikeService
    {
         Task SendLike(int fromId, int toId);
         Task GetbackLike(int fromId, int toId);
    }
}