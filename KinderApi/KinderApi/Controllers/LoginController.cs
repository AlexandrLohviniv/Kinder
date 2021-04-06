using System.Threading.Tasks;
using KinderApi.DTOs;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<bool> LogIn([FromBody]LoginDto loginData)
        {   
            return await loginService.LoginUser(loginData.Mail, loginData.Password);
        }
    }
}