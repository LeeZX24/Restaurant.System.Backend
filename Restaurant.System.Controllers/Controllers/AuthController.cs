using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Services.Interfaces;
using Restaurant.System.Models.Enums;

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
        public async Task<ActionResult<UserDto>> Login([FromBody] UserDto loginDetails)
        {
            try
            {
                var response = await _authService.LoginAsync(loginDetails);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal server error" }); // 500
            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserDto registerDetails)
        {
            try
            {
                var res = await _authService.RegisterAsync(registerDetails);
                
                if(res.Status != Status.Success) return BadRequest(new { Message = res.ResponseDetails.Message });
                
                return Ok(res);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal server error" }); // 500
            }

        }
    }
}


