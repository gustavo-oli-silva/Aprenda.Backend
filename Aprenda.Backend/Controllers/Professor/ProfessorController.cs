using Aprenda.Backend.Repositories.Homework;
using Aprenda.Backend.Services.Homework;
using Aprenda.Backend.Services.Post;
using Aprenda.Backend.Services.Submission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Professor")]
    public class ProfessorController : ControllerBase
    {

        private readonly IPostService _PostService;
        private readonly IHomeworkService _HomeworkService;
        private readonly ISubmissionService _SubmissionService;
        private readonly ILogger<ProfessorController> _logger;

        public ProfessorController(IPostService PostService, IHomeworkService HomeworkService, ISubmissionService SubmissionService, ILogger<ProfessorController> logger)
        {
            _PostService = PostService;
            _HomeworkService = HomeworkService;
            _SubmissionService = SubmissionService;
            _logger = logger;
        }

        [Authorize(Roles = "Professor")]
        [HttpPost("classrooms/{classroomId}/posts")]
        public async Task<IActionResult> CreatePost(long classroomId, [FromBody] Dtos.Post.CreatePostDto createDto)
        {
            _logger.LogInformation("Professor creating post in classroom {ClassroomId}", classroomId);
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            var post = await _PostService.CreatePostAsync(userId, classroomId, createDto);
            return CreatedAtAction(nameof(CreatePost), new { userIdString, classroomId }, post);
        }

        [Authorize(Roles = "Professor")]
        [HttpPost("classrooms/{classroomId}/homeworks")]
        public async Task<IActionResult> CreateHomework(long classroomId, [FromBody] Dtos.Homework.CreateHomeworkDto createDto)
        {
            _logger.LogInformation("Professor creating homework in classroom {ClassroomId}", classroomId);
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            var homework = await _HomeworkService.CreateHomeworkAsync(userId, classroomId, createDto);
            return CreatedAtAction(nameof(CreateHomework), new { classroomId }, homework);
        }

        [Authorize(Roles = "Professor")]
        [HttpGet("classrooms/{classroomId}/homeworks")]
        public async Task<IActionResult> GetAllHomeworksByClassroomId(long classroomId)
        {
            _logger.LogInformation("Retrieving all homeworks for classroom {ClassroomId}", classroomId);
            var posts = await _HomeworkService.GetAllHomeworksByClassroomIdAsync(classroomId);
            return Ok(posts);
        }


        [Authorize(Roles = "Professor")]
        [HttpGet("classrooms/{classroomId}/posts")]
        public async Task<IActionResult> GetAllPostsByClassroomId(long classroomId)
        {
            _logger.LogInformation("Retrieving all posts for classroom {ClassroomId}", classroomId);
            var posts = await _PostService.GetAllPostsByClassroomIdAsync(classroomId);
            return Ok(posts);
        }

        [Authorize(Roles = "Professor")]
        [HttpDelete("posts/{postId}")]
        public async Task<IActionResult> DeletePost(long postId)
        {
            _logger.LogInformation("Professor deleting post {PostId}", postId);
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            await _PostService.DeletePostAsync(userId, postId);
            return NoContent();
        }

        [Authorize(Roles = "Professor")]
        [HttpPut("posts/{postId}")]
        public async Task<IActionResult> UpdatePost(long postId, [FromBody] Dtos.Post.CreatePostDto updateDto)
        {
            _logger.LogInformation("Professor updating post {PostId}", postId);
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            long userId = long.Parse(userIdString);
            await _PostService.UpdatePostAsync(userId, postId, updateDto);
            return NoContent();
        }

        [Authorize(Roles = "Professor")]
        [HttpGet("homeworks/{homeworkId}/submissions")]
        public async Task<IActionResult> GetAllSubmissionsByHomeworkId(long homeworkId)
        {
            _logger.LogInformation("Professor retrieving submissions for homework {HomeworkId}", homeworkId);
            var submissions = await _SubmissionService.GetAllSubmissionsByHomeworkIdAsync(homeworkId);
            return Ok(submissions);
        }

    }
}
