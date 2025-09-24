using Aprenda.Backend.Services.Classroom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {

        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classrooms = await _classroomService.GetAllClassroomsAsync();
            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var classroom = await _classroomService.GetClassroomByIdAsync(id);
            return Ok(classroom);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dtos.Classroom.CreateClassroomDto createDto)
        {
            var classroom = await _classroomService.CreateClassroomAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = classroom.Id }, classroom);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Dtos.Classroom.CreateClassroomDto updateDto)
        {
            await _classroomService.UpdateClassroomAsync(id, updateDto);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _classroomService.DeleteClassroomAsync(id);
            return NoContent();
        }
    }
}
