using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cors")]
public class TestCorsController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("test")]
    public IActionResult GetTest() => Ok(new { ok = true });
    
    [AllowAnonymous]
    [HttpPost("test")]
    public IActionResult PostTest() => Ok(new { ok = true });
}