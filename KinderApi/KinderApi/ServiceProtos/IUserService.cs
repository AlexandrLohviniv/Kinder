using System.Collections.Generic;
using System.Threading.Tasks;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<Image> GetMainPhotoByUser(int userId);
        Task<Image> GetUserPhotoByPhotoId(int userId, int photoId);
    }
}