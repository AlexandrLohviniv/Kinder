using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.helper;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KinderApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MatchPageController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public MatchPageController(DatabaseContext context, IUserService userService, IMapper mapper)
        {
            this.context = context;
            this.userService = userService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "SimpleUser,Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUSers([FromQuery]PaginationParams userParams)
        {
            PagedList<User> allUsers = await userService.GetAllUsers(userParams);
            List<UserToReturnDto> returnUsers = mapper.Map<List<UserToReturnDto>>(allUsers);

            Response.AddPagination(allUsers.CurrentPage,allUsers.PageSize, 
                allUsers.TotalCount, allUsers.TotalPages);

            return Ok(returnUsers);
        }

        [HttpGet("{userId}/nearByDistanceUsers/{distance?}")]
        public async Task<IActionResult> GetMatchUsersByDistance(int userId,int? distance)
        {
            List<User> allUsers = await userService.GetUsersForMatchByDistance(userId, distance);
            List<UserToReturnDto> returnUsers = mapper.Map<List<UserToReturnDto>>(allUsers);

            return Ok(returnUsers);
        }

        [HttpPost("{userId}/nearByPreferenceUsers")]
        public async Task<IActionResult> GetMatchUsersByPreference(int userId)
        {
            string body;
            PreferenceDto userPref = null;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }

            if (!string.IsNullOrEmpty(body))
            {
                userPref = JsonConvert.DeserializeObject<PreferenceDto>(body);
            }
        

            List<User> allUsers = await userService.GetUsersForMathcByPreference(userId, userPref);
            List<UserToReturnDto> returnUsers = mapper.Map<List<UserToReturnDto>>(allUsers);

            return Ok(returnUsers);
        }
    }
}