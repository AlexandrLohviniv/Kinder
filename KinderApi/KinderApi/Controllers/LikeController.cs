using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]/{userId}")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService likeService;

        public LikeController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpPost("like/{receiverId}")]
        public async Task<IActionResult> SendLike(int userId, int receiverId)
        {
            await likeService.SendLike(userId,receiverId);
            return Ok();
        }

        [HttpPost("unlike/{receiverId}")]
        public async Task<IActionResult> GetbackLike(int userId, int receiverId)
        {
            await likeService.GetbackLike(userId, receiverId);
            return Ok();
        }
    }
}