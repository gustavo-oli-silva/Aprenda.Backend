using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Services.Homework;
using Aprenda.Backend.Services.Post;
using Aprenda.Backend.Services.Submission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authorization;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Professor")]
    public class ProfessorController : ControllerBase
    {

        private readonly IPostService _PostService;
        private readonly IHomeworkService _HomeworkService;

        private readonly ISubmissionService _SubmissionService;

        public ProfessorController(IPostService PostService, IHomeworkService HomeworkService, ISubmissionService SubmissionService)
        {
            _PostService = PostService;
            _HomeworkService = HomeworkService;
            _SubmissionService = SubmissionService;
        }


        [HttpPost("classrooms/{classroomId}/posts")]
        public async Task<IActionResult> CreatePost(long classroomId, [FromBody] Dtos.Post.CreatePostDto createDto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            var post = await _PostService.CreatePostAsync(userId, classroomId, createDto);
            return CreatedAtAction(nameof(CreatePost), new { userIdString, classroomId }, post);
        }

        [HttpPost("{professorId}/classrooms/{classroomId}/homeworks")]
        public async Task<IActionResult> CreateHomework(long professorId, long classroomId, [FromBody] Dtos.Homework.CreateHomeworkDto createDto)
        {
            var homework = await _HomeworkService.CreateHomeworkAsync(professorId, classroomId, createDto);
            return CreatedAtAction(nameof(CreateHomework), new { professorId, classroomId }, homework);
        }

        [HttpGet("classrooms/{classroomId}/homeworks")]
        public async Task<IActionResult> GetAllHomeworksByClassroomId(long classroomId)
        {
            var posts = await _HomeworkService.GetAllHomeworksByClassroomIdAsync(classroomId);
            return Ok(posts);
        }


        [HttpGet("classrooms/{classroomId}/posts")]
        public async Task<IActionResult> GetAllPostsByClassroomId(long classroomId)
        {
            var posts = await _PostService.GetAllPostsByClassroomIdAsync(classroomId);
            return Ok(posts);
        }

        [HttpDelete("{professorId}/posts/{postId}")]
        public async Task<IActionResult> DeletePost(long professorId, long postId)
        {
            await _PostService.DeletePostAsync(professorId, postId);
            return NoContent();
        }

        [HttpPut("{professorId}/posts/{postId}")]
        public async Task<IActionResult> UpdatePost(long professorId, long postId, [FromBody] Dtos.Post.CreatePostDto updateDto)
        {
            await _PostService.UpdatePostAsync(professorId, postId, updateDto);
            return NoContent();
        }

        [HttpGet("homeworks/{homeworkId}/submissions")]
        public async Task<IActionResult> GetAllSubmissionsByHomeworkId(long homeworkId)
        {
            var submissions = await _SubmissionService.GetAllSubmissionsByHomeworkIdAsync(homeworkId);
            return Ok(submissions);
        }

    }
}
