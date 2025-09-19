using Microsoft.AspNetCore.Mvc;

namespace Aprenda.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TesteController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return NoContent();
    }
}