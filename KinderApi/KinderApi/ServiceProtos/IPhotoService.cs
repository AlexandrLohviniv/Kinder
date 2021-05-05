using System.Threading.Tasks;
using KinderApi.DTOs;
using Microsoft.AspNetCore.Http;

namespace KinderApi.ServiceProtos
{
    public interface IPhotoService
    {
         Task<ImageDto> addPhoto(IFormFile photo);
         Task<bool> deletePhoto(string photoPublicId);
    }
}