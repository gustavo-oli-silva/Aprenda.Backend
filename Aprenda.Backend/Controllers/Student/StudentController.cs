using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Services.Homework;
using Aprenda.Backend.Services.Post;
using Aprenda.Backend.Services.Submission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly ISubmissionService _SubmissionService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(ISubmissionService SubmissionService, ILogger<StudentController> logger)
        {
            _SubmissionService = SubmissionService;
            _logger = logger;
        }


        [Authorize(Roles = "Student")]
        [HttpPost("homeworks/{homeworkId}/submissions")]
        public async Task<IActionResult> CreateSubmission(long homeworkId, [FromBody] Dtos.Submission.CreateSubmissionDto createDto)
        {
            _logger.LogInformation("Student creating submission for homework {HomeworkId}", homeworkId);
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            var submission = await _SubmissionService.CreateSubmissionAsync(userId, homeworkId, createDto);
            return CreatedAtAction(nameof(CreateSubmission), new { homeworkId }, submission);
        }

       

        [Authorize(Roles = "Student")]
        [HttpGet("homeworks/{homeworkId}/submissions")]
        public async Task<IActionResult> GetAllSubmissionsByHomeworkId(long homeworkId)
        {
            _logger.LogInformation("Student retrieving submissions for homework {HomeworkId}", homeworkId);
            var submissions = await _SubmissionService.GetAllSubmissionsByHomeworkIdAsync(homeworkId);
            return Ok(submissions);
        }



    }
}
