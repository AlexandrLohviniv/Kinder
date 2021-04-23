using System.Collections.Generic;
using System.Threading.Tasks;
using KinderApi.DTOs;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<Image> GetMainPhotoByUser(int userId);
        Task<Image> GetUserPhotoByPhotoId(int userId, int photoId);
        Task<List<User>> GetUsersForMatchByDistance(int currentUserId, int? distance);
        Task<List<User>> GetUsersForMathcByPreference(int currentUserId, PreferenceDto prefernces = null);
    }
}