using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Services.Interfaces;
using Restaurant.System.Models.Enums;
using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using System.Diagnostics;

namespace Restaurant.System.Controllers.Controllers
{
    [ApiController]
    [Route("api/maintenance/dropdown")]
    public class DropdownMaintenanceController : ControllerBase
    {
        private readonly IDropdownMaintenanceService _dropdownMaintenanceService;

        public DropdownMaintenanceController(IDropdownMaintenanceService dropdownMaintenanceService)
        {
            _dropdownMaintenanceService = dropdownMaintenanceService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<DropdownDto>> GetDropdownList()
        {
            try
            {
                var res = await _dropdownMaintenanceService.GetDropdownListAsync();

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
        public async Task<ActionResult<DropdownDto>> AddNewDropdown([FromBody] DropdownDto dropdownData)
        {
            try
            {
                var res = await _dropdownMaintenanceService.AddNewDropdownAsync(dropdownData);

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

        [HttpPost("update")]
        public async Task<ActionResult<DropdownDto>> EditDropdownDetails([FromBody] DropdownDto DropdownDetails)
        {
            try
            {
                var res = await _dropdownMaintenanceService.UpdateDropdownDetailsAsync(DropdownDetails);

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

        [HttpPost("delete")]
        public async Task<ActionResult<DropdownDto>> RemoveDropdown([FromBody] DropdownDto dropdownData)
        {
            try
            {
                var res = await _dropdownMaintenanceService.DeleteDropdownAsync(dropdownData);

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


