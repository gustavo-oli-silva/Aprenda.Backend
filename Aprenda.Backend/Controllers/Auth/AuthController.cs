using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Services.Homework;
using Aprenda.Backend.Services.Jwt;
using Aprenda.Backend.Services.Post;
using Aprenda.Backend.Services.Submission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Aprenda.Backend.Services.User;
using Microsoft.AspNetCore.Authorization;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IUserService userService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _userService = userService;
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


        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            _logger.LogInformation("User attempting to retrieve profile information");
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

    }
}
