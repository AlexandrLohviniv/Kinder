using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using KinderApi.DTOs;
using KinderApi.ServiceProtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace KinderApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController:ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody]LoginDto loginData)
        {   
            

            ClaimsIdentity identity = await GetIdentity(loginData.Mail, loginData.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new 
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return new JsonResult(response);
        }

        private async Task<ClaimsIdentity>  GetIdentity(string mail, string password)
        {

            if(await loginService.LoginUser(mail, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, "User 1"),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin") 
                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}