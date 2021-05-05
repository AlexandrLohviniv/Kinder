using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using KinderApi.DTOs;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Http;

namespace KinderApi.Services
{
    public class PhotoService : IPhotoService
    {
        readonly string cloudName = "dwwbgyepf";
        readonly string api_key = "798622644567836";
        readonly string api_secret = "XejnP_0SmQe3yAjLUhzHSWgTeX8";

        Account account;
        Cloudinary cloudinary;

        public PhotoService()
        {
            account = new Account(cloudName, api_key, api_secret);

            cloudinary = new Cloudinary(account);
        }

        public async Task<ImageDto> addPhoto(IFormFile photo)
        {
            var file = photo;

            var uploadResults = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResults = await cloudinary.UploadAsync(uploadParams);
                }
            }
            else
            {
                return null;
            }

            ImageDto image = new ImageDto()
            {
                ImagePublicId = uploadResults.PublicId,
                ImgPath = uploadResults.Url.ToString()
            };

            return image;
        }

        public async Task<bool> deletePhoto(string photoPublicId)
        {
            try
            {
                var deleteParams = new DeletionParams(photoPublicId);

                var result = await cloudinary.DestroyAsync(deleteParams);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}