
using System.Threading.Tasks;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController:ControllerBase
    {
        private readonly ILoginService loginService;

        public RegisterController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            return Ok();
        }
    }
}