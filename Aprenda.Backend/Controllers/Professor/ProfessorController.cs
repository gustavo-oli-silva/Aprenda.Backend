using Aprenda.Backend.Services.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {

        private readonly IPostService _PostService;

        public ProfessorController(IPostService PostService)
        {
            _PostService = PostService;
        }


        [HttpPost("{professorId}/classrooms/{classroomId}/posts")]
        public async Task<IActionResult> CreatePost(long professorId, long classroomId, [FromBody] Dtos.Post.CreatePostDto createDto)
        {
            await _PostService.CreatePostAsync(professorId, classroomId, createDto);
            return CreatedAtAction(nameof(CreatePost), new { professorId, classroomId }, createDto);
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

    }
}
