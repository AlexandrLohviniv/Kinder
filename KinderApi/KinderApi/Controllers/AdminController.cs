using System;
using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly IUserService userService;

        public AdminController(DatabaseContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }


        [HttpPost]
        [Route("ban/{userId}")]
        public async Task<IActionResult> BanUser(int userId, [FromBody] DateTime lastDay)
        {
            User userToBan = await userService.GetUserById(userId);

            if (userToBan == null)
                return BadRequest();

            if(await context.BannedUsers.AnyAsync(b => b.UserId == userId))
                return BadRequest("You have already banned this user");

            await context.BannedUsers.AddAsync(new BannedUsers() { User = userToBan, LastBanDay = lastDay });
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("unban/{userId}")]
        public async Task<IActionResult> UnbanUser(int userId, [FromBody] DateTime lastDay)
        {
            BannedUsers userToUnBan = await context.BannedUsers.AsQueryable().FirstOrDefaultAsync(u => u.UserId == userId);

            if (userToUnBan == null)
                return BadRequest("There is no such banned user");

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
    }
}