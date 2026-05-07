using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Services.Interfaces;
using Restaurant.System.Models.Enums;
using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;

namespace Restaurant.System.Controllers.Controllers
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

        [HttpPost("add")]
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
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal server error" }); // 500
            }
        }

        [HttpPost("edit")]
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

        [HttpPost("remove")]
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


