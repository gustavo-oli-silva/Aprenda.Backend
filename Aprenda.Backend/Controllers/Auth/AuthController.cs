using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Services.Homework;
using Aprenda.Backend.Services.Jwt;
using Aprenda.Backend.Services.Post;
using Aprenda.Backend.Services.Submission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
    
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Dtos.User.LoginDto loginDto)
        {
            _logger.LogInformation("User attempting login with email {Email}", loginDto.Email);
            var token = await _authService.Authenticate(loginDto);
            if (token == null)
            {
                _logger.LogWarning("Failed login attempt for email {Email}", loginDto.Email);
                return Unauthorized(new { message = "Invalid email or password" });
            }
            _logger.LogInformation("Successful login for email {Email}", loginDto.Email);
            return Ok(new { Token = token });
        }

    }
}
