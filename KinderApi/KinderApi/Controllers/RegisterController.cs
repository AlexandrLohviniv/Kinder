
using System;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IMapper mapper;

        public RegisterController(ILoginService loginService, IMapper mapper)
        {
            this.mapper = mapper;
            this.loginService = loginService;
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto user)
        {
            User currentUser = mapper.Map<User>(user);
            currentUser.LastSeen = DateTime.Now;
            User registeredUser = await loginService.RegisterUSer(currentUser);

            if(registeredUser == null)
                return BadRequest();

            UserToReturnDto returnUsers = mapper.Map<UserToReturnDto>(registeredUser);
            return Ok(registeredUser);
        }
    }
}