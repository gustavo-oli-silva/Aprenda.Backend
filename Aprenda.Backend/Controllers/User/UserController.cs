using Aprenda.Backend.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _UserService;

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Users = await _UserService.GetAllUsersAsync();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var User = await _UserService.GetUserByIdAsync(id);
            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dtos.User.CreateUserDto createDto)
        {
            var User = await _UserService.CreateUserAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = User.Id }, User);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Dtos.User.CreateUserDto updateDto)
        {
            await _UserService.UpdateUserAsync(id, updateDto);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _UserService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
