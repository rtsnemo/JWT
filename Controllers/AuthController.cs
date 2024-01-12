using JWT.Data;
using JWT.Models;
using JWT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWT.Controllers
{
    [Route("/")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly RegService _regService;

        public AuthController(AuthService authService, RegService regService)
        {
            _authService = authService;
            _regService = regService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            var token = _authService.AuthenticateUser(loginModel.Name, loginModel.EnteredPassword);

            if (token != null)
            {
                return Ok(new { token });
            }

            return Unauthorized(new { error = "Invalid username or password" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegModel regModel)
        {

            return Ok(_regService.RegisterUserAsync(regModel).Result);
        }

        [Authorize]
        [HttpPost("test")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
