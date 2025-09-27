using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Services.Homework;
using Aprenda.Backend.Services.Post;
using Aprenda.Backend.Services.Submission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly ISubmissionService _SubmissionService;

        public StudentController(ISubmissionService SubmissionService)
        {
            _SubmissionService = SubmissionService;
        }


        [HttpPost("{studentId}/homeworks/{homeworkId}/submissions")]
        public async Task<IActionResult> CreateSubmission(long studentId, long homeworkId, [FromBody] Dtos.Submission.CreateSubmissionDto createDto)
        {
            var submission = await _SubmissionService.CreateSubmissionAsync(studentId, homeworkId, createDto);
            return CreatedAtAction(nameof(CreateSubmission), new { studentId, homeworkId }, submission);
        }

       

        [HttpGet("homeworks/{homeworkId}/submissions")]
        public async Task<IActionResult> GetAllSubmissionsByHomeworkId(long homeworkId)
        {
            var submissions = await _SubmissionService.GetAllSubmissionsByHomeworkIdAsync(homeworkId);
            return Ok(submissions);
        }



    }
}
