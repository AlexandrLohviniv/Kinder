using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserController(  DatabaseContext context, 
                                IMapper mapper,
                                IUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpGet("{userId}/Info")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await userService.GetUserById(userId);

            UserToReturnDto userToReturn = mapper.Map<UserToReturnDto>(user);

            return Ok(userToReturn);
        }


        [HttpGet("{userId}/Preference")]
        public async Task<IActionResult> GetUserPreferenceById(int userId)
        {
            var user = await userService.GetUserById(userId);

            var preferences = mapper.Map<PreferenceDto>(user.Preferences.First());

            return Ok(preferences);
        }

        [HttpPost("{userId}/UpdateInfo")]
        public async Task<IActionResult> UpdateUserInfo(int userId, [FromBody] UpdateUserDto newUser)
        {
            var user = await userService.GetUserById(userId);

            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.Sex = newUser.Sex;
            user.DateOfBith = newUser.DateOfBith;
            user.AboutMe = newUser.AboutMe;
            user.NickName = newUser.NickName;

            user.Preferences.Remove(user.Preferences.First());
            user.Preferences.Add(mapper.Map<Preference>(newUser.Preferences));

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}