using Aprenda.Backend.Services;
using Aprenda.Backend.Services.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {

        private readonly IArchiveService _ArchiveService;

        public ArchiveController(IArchiveService ArchiveService)
        {
            _ArchiveService = ArchiveService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var resultDto = await _ArchiveService.UploadFileAsync(file);
                return Ok(resultDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Erro interno do servidor ao salvar o arquivo: {ex.Message}");
            }
        }

        [HttpGet("download/{storedName}")]
        public async Task<IActionResult> DownloadFile(string storedName)
        {
            try
            {
                var (content, contentType, originalName) = await _ArchiveService.GetFileForDownloadAsync(storedName);
                return File(content, contentType, originalName);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno ao processar o arquivo." });
            }
        }
    }
}
