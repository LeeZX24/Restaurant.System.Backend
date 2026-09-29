using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Controllers.Controllers.Base;

[ApiController]
[Route("cors")]
public class TestCorsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("test")]
    public IActionResult GetTest() => Ok(new { ok = true });
    
    [AllowAnonymous]
    [HttpPost("test")]
    public IActionResult PostTest() => Ok(new { ok = true });
}