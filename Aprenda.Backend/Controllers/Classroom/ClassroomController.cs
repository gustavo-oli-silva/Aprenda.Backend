using Aprenda.Backend.Services.Classroom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aprenda.Backend.Controllers
{
    // [Authorize]
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

        [HttpPost("{classroomId}/users/{userId}")]
        public async Task<IActionResult> AssignUser(long classroomId, long userId)
        {
            await _classroomService.AssignUserToClassroom(classroomId, userId);
            return NoContent();
        }

        [HttpPost("{classroomId}/students")]
        public async Task<IActionResult> GetStudents(long classroomId)
        {
            var students = await _classroomService.GetClassroomWithStudentsAsync(classroomId);
            return Ok(students);
        }
        [HttpPost("{classroomId}/professors")]
        public async Task<IActionResult> GetProfessors(long classroomId)
        {
            var professors = await _classroomService.GetClassroomWithProfessorsAsync(classroomId);
            return Ok(professors);
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
