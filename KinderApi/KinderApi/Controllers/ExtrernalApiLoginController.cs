using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.helper;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KinderApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ExtrernalApiLoginController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;
        private readonly ILoginService loginSerivce;

        public ExtrernalApiLoginController(DatabaseContext context, IMapper mapper, ILoginService loginSerivce)
        {
            this.context = context;
            this.mapper = mapper;
            this.loginSerivce = loginSerivce;
        }

        [HttpPost("apilogin/{email}")]
        public async Task<IActionResult> ApiLogin(string email)
        {
            string password = MyEncoder.ComputeSha256Hash(email);

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null)
                return BadRequest("There is no such user");

            return Ok("Well done");
        }

        [HttpPost("apilogin/{email}")]
        public async Task<IActionResult> RegisterApi([FromBody] RegisterUserDto user)
        {
            user.Password = MyEncoder.ComputeSha256Hash(user.Email);

            User currentUser = mapper.Map<User>(user);
            currentUser.LastSeen = DateTime.Now;
            User registeredUser = await loginSerivce.RegisterUSer(currentUser);

            if (registeredUser == null)
                return BadRequest();

            UserToReturnDto returnUsers = mapper.Map<UserToReturnDto>(registeredUser);
            return Ok(registeredUser);
        }
    }
}