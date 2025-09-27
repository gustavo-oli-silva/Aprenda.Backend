using Aprenda.Backend.Services;
using Aprenda.Backend.Services.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aprenda.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {

        private readonly IArchiveService _ArchiveService;
        private readonly ILogger<ArchiveController> _logger;

        public ArchiveController(IArchiveService ArchiveService, ILogger<ArchiveController> logger)
        {
            _ArchiveService = ArchiveService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            _logger.LogInformation("User uploading file {FileName}", file?.FileName);
            try
            {
                var resultDto = await _ArchiveService.UploadFileAsync(file);
                return Ok(resultDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid file upload attempt: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "Error saving uploaded file");
                return StatusCode(500, $"Erro interno do servidor ao salvar o arquivo: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("download/{storedName}")]
        public async Task<IActionResult> DownloadFile(string storedName)
        {
            _logger.LogInformation("User downloading file {StoredName}", storedName);
            try
            {
                var (content, contentType, originalName) = await _ArchiveService.GetFileForDownloadAsync(storedName);
                return File(content, contentType, originalName);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning("File not found: {StoredName} - {ErrorMessage}", storedName, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading file {StoredName}", storedName);
                return StatusCode(500, new { message = "Ocorreu um erro interno ao processar o arquivo." });
            }
        }
    }
}
