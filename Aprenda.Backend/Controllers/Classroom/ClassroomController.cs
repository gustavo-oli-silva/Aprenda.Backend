using Aprenda.Backend.Services.Classroom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Aprenda.Backend.Services.Homework;

namespace Aprenda.Backend.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {

        private readonly IClassroomService _classroomService;

        private readonly IHomeworkService _homeworkService;
        private readonly ILogger<ClassroomController> _logger;

        public ClassroomController(IClassroomService classroomService, IHomeworkService homeworkService, ILogger<ClassroomController> logger)
        {
            _classroomService = classroomService;
            _homeworkService = homeworkService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Retrieving all classrooms");
            var classrooms = await _classroomService.GetAllClassroomsAsync();
            return Ok(classrooms);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Retrieving classroom {ClassroomId}", id);
            var classroom = await _classroomService.GetClassroomByIdAsync(id);
            return Ok(classroom);
        }

        [Authorize]
        [HttpGet("homework/{idHomework}")]
        public async Task<IActionResult> GetHomeworkById(long idHomework)
        {
            _logger.LogInformation("Retrieving homework {HomeworkId}", idHomework);
            var homework = await _homeworkService.GetHomeworkByIdAsync(idHomework);
            return Ok(homework);
        }

        

        [Authorize]
        [HttpPost("{inviteCode}/join")]
        public async Task<IActionResult> JoinClassroom(string inviteCode)
        {
            _logger.LogInformation("User joining classroom with invite code {InviteCode}", inviteCode);
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            await _classroomService.JoinClassroom(inviteCode, userId);
            return NoContent();
        }

        [Authorize]
        [HttpGet("{classroomId}/students")]
        public async Task<IActionResult> GetStudents(long classroomId)
        {
            _logger.LogInformation("Retrieving students for classroom {ClassroomId}", classroomId);
            var students = await _classroomService.GetClassroomWithStudentsAsync(classroomId);
            return Ok(students);
        }
        [Authorize]
        [HttpGet("{classroomId}/professors")]
        public async Task<IActionResult> GetProfessors(long classroomId)
        {
            _logger.LogInformation("Retrieving professors for classroom {ClassroomId}", classroomId);
            var professors = await _classroomService.GetClassroomWithProfessorsAsync(classroomId);
            return Ok(professors);
        }
        [Authorize(Roles = "Professor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dtos.Classroom.CreateClassroomDto createDto)
        {
            _logger.LogInformation("Creating new classroom with name {ClassroomName}", createDto.Name);
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            var classroom = await _classroomService.CreateClassroomAsync(userId, createDto);
            return CreatedAtAction(nameof(GetById), new { id = classroom.Id }, classroom);
        }

        [Authorize(Roles = "Professor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Dtos.Classroom.CreateClassroomDto updateDto)
        {
            _logger.LogInformation("Updating classroom {ClassroomId}", id);
            await _classroomService.UpdateClassroomAsync(id, updateDto);
            return NoContent();
        }

        [Authorize(Roles = "Professor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("Deleting classroom {ClassroomId}", id);
            await _classroomService.DeleteClassroomAsync(id);
            return NoContent();
        }


    }
}
