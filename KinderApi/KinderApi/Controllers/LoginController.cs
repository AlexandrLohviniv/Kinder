using System;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using KinderApi.DTOs;
using KinderApi.ServiceProtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using KinderApi.Models;
using Newtonsoft.Json;
using System.Text;
using KinderApi.helper;

namespace KinderApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController:ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly DatabaseContext context;

        public LoginController(ILoginService loginService, DatabaseContext context)
        {
            this.loginService = loginService;
            this.context = context;
            
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody]LoginDto loginData)
        {
          
            User user = await loginService.LoginUser(loginData.Mail, MyEncoder.ComputeSha256Hash(loginData.Password));

            if (user == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

             var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()) 
                };

            var key = AuthOptions.GetSymmetricSecurityKey();

            var creds = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecriptor);

            return Ok(new {token = tokenHandler.WriteToken(token)});
        }
    }
}