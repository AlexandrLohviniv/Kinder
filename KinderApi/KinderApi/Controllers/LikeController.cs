using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]/{userId}")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService likeService;
        private readonly IMapper mapper;

        public LikeController(ILikeService likeService, IMapper mapper)
        {
            this.likeService = likeService;
            this.mapper = mapper;
        }

        [HttpPost("like/{receiverId}")]
        [Authorize(Roles = "SimpleUser,Admin")]
        public async Task<IActionResult> SendLike(int userId, int receiverId)
        {
            await likeService.SendLike(userId,receiverId);
            return Ok();
        }

        [HttpPost("unlike/{receiverId}")]
        [Authorize(Roles = "SimpleUser,Admin")]
        public async Task<IActionResult> GetbackLike(int userId, int receiverId)
        {
            await likeService.GetbackLike(userId, receiverId);
            return Ok();
        }

        [HttpGet("pairs")]
        [Authorize(Roles = "SimpleUser,Admin")]
        public async Task<IActionResult> GetUserPairs(int userId)
        {
            var users = await likeService.PairsForUser(userId);
            var usersToReturn = mapper.Map<List<UserToReturnDto>>(users);

            return Ok(usersToReturn);
        }
    }
}