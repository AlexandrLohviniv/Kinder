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
    [Route("[controller]")]
    public class MatchPageController:ControllerBase
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

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUSers()
        {
            List<User> allUsers = await userService.GetAllUsers();
            List<UserToReturnDto> returnUsers = mapper.Map<List<UserToReturnDto>>(allUsers);

            return Ok(returnUsers);
        }
    }
}