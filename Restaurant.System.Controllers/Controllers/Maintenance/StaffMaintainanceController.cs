using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Models.Enums;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Services.Interfaces.Maintenance;

namespace Restaurant.System.Controllers.Controllers.Maintenance
{
    [ApiController]
    [Route("api/maintenance/staff")]
    public class StaffMaintenanceController : ControllerBase
    {
        private readonly IStaffMaintenanceService _staffMaintenanceService;

        public StaffMaintenanceController(IStaffMaintenanceService staffMaintenanceService)
        {
            _staffMaintenanceService = staffMaintenanceService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<UserDto>> GetStaffList()
        {
            try
            {
                var res = await _staffMaintenanceService.GetStaffListAsync();

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

        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> AddNewStaff([FromBody] StaffDto staffDetails)
        {
            try
            {
                var res = await _staffMaintenanceService.AddNewStaffAsync(staffDetails);

                if (res.Status != Status.Success) return BadRequest(new { Message = res.ResponseDetails.Message });

                return Ok(res);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message, StackTrace = ex.StackTrace, innerMessage = ex.InnerException?.Message });
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<UserDto>> EditStaffDetails([FromBody] StaffDto staffDetails)
        {
            try
            {
                var res = await _staffMaintenanceService.UpdateStaffDetailsAsync(staffDetails);

                if (res.Status != Status.Success) return BadRequest(new { Message = res.ResponseDetails.Message });

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

        [HttpDelete("delete")]
        public async Task<ActionResult<UserDto>> RemoveStaff([FromBody] StaffDto staffDetails)
        {
            try
            {
                var res = await _staffMaintenanceService.DeleteStaffAsync(staffDetails.Username);

                if (res.Status != Status.Success) return BadRequest(new { Message = res.ResponseDetails.Message });

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


