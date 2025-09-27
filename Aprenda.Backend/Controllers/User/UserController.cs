using Aprenda.Backend.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _UserService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService UserService, ILogger<UserController> logger)
        {
            _UserService = UserService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Retrieving all users");
            var Users = await _UserService.GetAllUsersAsync();
            return Ok(Users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Retrieving user {UserId}", id);
            var User = await _UserService.GetUserByIdAsync(id);
            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dtos.User.CreateUserDto createDto)
        {
            _logger.LogInformation("Creating new user with email {UserEmail}", createDto.Email);
            var User = await _UserService.CreateUserAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = User.Id }, User);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Dtos.User.CreateUserDto updateDto)
        {
            _logger.LogInformation("Updating user {UserId}", id);
            await _UserService.UpdateUserAsync(id, updateDto);
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("Deleting user {UserId}", id);
            await _UserService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
