using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aprenda.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TesteController : ControllerBase
{
    private readonly ILogger<TesteController> _logger;

    public TesteController(ILogger<TesteController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Test endpoint accessed");
        return NoContent();
    }
}