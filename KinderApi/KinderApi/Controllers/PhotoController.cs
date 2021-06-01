using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]/{userId}")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService photoService;
        private readonly DatabaseContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public PhotoController(IPhotoService photoService,
                            DatabaseContext context,
                            IMapper mapper,
                            IUserService userService)
        {
            this.photoService = photoService;
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }
        
        [HttpPost("AddPhoto")]
        public async Task<IActionResult> AddPhoto(int userId, [FromForm] IFormFile file)
        {

            ImageDto uploadedImage = await photoService.addPhoto(file);

            if (uploadedImage == null)
                return BadRequest();

            var user = await userService.GetUserById(userId);

            if (user == null)
                return BadRequest();

            if (!user.Images.Any(i => i.IsMain))
            {
                uploadedImage.IsMain = true;
            }
            
            Image image = mapper.Map<Image>(uploadedImage);
            image.User = user;
            try
            {
                context.Image.Add(image);

                await context.SaveChangesAsync();
                return Ok(image);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Authorize]
        [HttpDelete("DeletePhoto/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int userId, int photoId)
        {
            var user = await userService.GetUserById(userId);

            if (user == null)
                return BadRequest("No valid user found");

            var photo = await userService.GetUserPhotoByPhotoId(userId, photoId);

            if (photo == null)
                return BadRequest("No valid photo found");
            if(photo.IsMain)
                return BadRequest("You can't delete main photo");

            bool success = await photoService.deletePhoto(photo.ImagePublicId);

            if (!success)
                return BadRequest("Something went wrong! Try again");


            try
            {
                context.Image.Remove(photo);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Authorize]
        [HttpPost("setMain/{photoId}")]
        public async Task<IActionResult> SetMainPhoto(int userId, int photoId)
        {
            var user = await userService.GetUserById(userId);

            if (user == null)
                return BadRequest("No valid user found");

            var photo = await userService.GetMainPhotoByUser(userId);

            if (photo == null)
                return BadRequest("No valid main photo found");

            var newMainPhoto = await userService.GetUserPhotoByPhotoId(userId, photoId); ;
            if (newMainPhoto == null)
                return BadRequest("No valid new main photo found");
            if(newMainPhoto.IsMain)
                return BadRequest("This photo is already main");


            photo.IsMain = false;
            newMainPhoto.IsMain = true;

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("getAllPhotos")]
        public async Task<IActionResult> GetAllPhoto(int userId)
        {
            var user = await userService.GetUserById(userId);

            if (user == null)
                return BadRequest("No valid user found");

            List<PhotoToReturnDto> photos = mapper.Map<List<PhotoToReturnDto>>(user.Images);

            return Ok(photos);
        }

        [Authorize]
        [HttpGet("getMainPhoto")]
        public async Task<IActionResult> GetMainPhoto(int userId)
        {
            var image = await userService.GetMainPhotoByUser(userId);

            if (image == null)
                return BadRequest("Something went wrong. Try again");

            PhotoToReturnDto imageToReturn = mapper.Map<PhotoToReturnDto>(image);

            return Ok(imageToReturn);
        }
    }
}