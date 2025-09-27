using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Services.Homework;
using Aprenda.Backend.Services.Jwt;
using Aprenda.Backend.Services.Post;
using Aprenda.Backend.Services.Submission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
    
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Dtos.User.LoginDto loginDto)
        {
            var token = await _authService.Authenticate(loginDto);
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
            return Ok(new { Token = token });
        }

    }
}
