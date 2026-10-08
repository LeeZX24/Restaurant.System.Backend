using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Controllers.Controllers.Base;
using Restaurant.System.Models.Dtos.Requests;
using Restaurant.System.Models.Dtos.Responses;
using Restaurant.System.Models.Enums;
using Restaurant.System.Services.Interfaces.Auth;

namespace Restaurant.System.Controllers.Controllers.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Auth API v1");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AppLoginResponse>> Login([FromBody] AppLoginRequest loginDetails)
        {
            try
            {
                Console.WriteLine("Details -> ", loginDetails);
                var response = await _authService.LoginAsync(loginDetails);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : ", ex.Message);
                return StatusCode(500, new { Message = "Internal server error" }); // 500
            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AppRegisterResponse>> Register([FromBody] AppRegisterRequest registerDetails)
        {
            try
            {
                var res = await _authService.RegisterAsync(registerDetails);

                if (res.Status != Status.Success) return BadRequest(new { Message = res.Message });

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

        [HttpPost("logout")]
        public async Task<ActionResult<AppLogoutResponse>> Logout([FromBody] AppLogoutRequest logoutDetails)
        {
            try
            {
                var res = await _authService.LogoutAsync(logoutDetails);

                if (res.Status != Status.Success) return BadRequest(new { Message = res.Message });

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


