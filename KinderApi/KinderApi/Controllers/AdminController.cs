using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KinderApi.helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly IUserService userService;
        private readonly IAdminService adminService;
        private readonly IMapper mapper;

        public AdminController(DatabaseContext context, IUserService userService,
         IAdminService adminService,
         IMapper mapper)
        {
            this.context = context;
            this.userService = userService;
            this.adminService = adminService;
            this.mapper = mapper;
        }


        [HttpPost]
        [Route("ban/{userId}")]
        public async Task<IActionResult> BanUser(int userId, [FromBody] BanUserData lastDay)
        {
            User userToBan = await userService.GetUserById(userId);

            if (userToBan == null)
                return BadRequest();

            if (await context.BannedUsers.AnyAsync(b => b.UserId == userId))
                return BadRequest("You have already banned this user");

            await context.BannedUsers.AddAsync(new BannedUsers() { User = userToBan, LastBanDay = lastDay.lastBanDay });
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("unban/{userId}")]
        public async Task<IActionResult> UnbanUser(int userId)
        {
            BannedUsers userToUnBan = await context.BannedUsers.AsQueryable().FirstOrDefaultAsync(u => u.UserId == userId);

            if (userToUnBan == null)
                return BadRequest("There is no such banned user");
           
            if(userToUnBan.LastBanDay > DateTime.Now)
                return BadRequest("You can't unban user before his time is over");

            context.BannedUsers.Remove(userToUnBan);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("upgardeRole/{userId}")]
        public async Task<IActionResult> UpgradeUserAccount(int userId, [FromBody] Role newRole)
        {
            User user = await userService.GetUserById(userId);

            if (user == null)
                return BadRequest("There is no such banned user");

            user.Role = newRole;
            await context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("bannedUsers")]
        public async Task<IActionResult> GetBannedUsers([FromQuery]PaginationParams userParams)
        {
            PagedList<User> users = await adminService.GetBannedUsers(userParams);

            List<UserToReturnDto> userToReturn = mapper.Map<List<UserToReturnDto>>(users);

             Response.AddPagination(users.CurrentPage,users.PageSize, 
                users.TotalCount, users.TotalPages);

            return Ok(userToReturn);
        }
    }
}