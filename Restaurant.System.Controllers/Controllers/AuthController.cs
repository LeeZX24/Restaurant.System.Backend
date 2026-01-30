using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Services.Interfaces;

namespace Restaurant.System.Controllers.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserDto loginDto)
        {
            var user = await _authService.LoginAsync(loginDto);
            if (user == null)
            return NotFound();
            

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] CustomerDto customer)
        {
            var user = await _authService.RegisterAsync(customer);
            return Ok(user);
        }
    }
}


