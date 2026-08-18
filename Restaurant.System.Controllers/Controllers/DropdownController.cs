using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Services.Interfaces;
using Restaurant.System.Models.Enums;
using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using System.Diagnostics;
using Restaurant.System.Services.Services;

namespace Restaurant.System.Controllers.Controllers
{
    [ApiController]
    [Route("api/dropdown")]
    public class DropdownController : ControllerBase
    {
        private readonly IDropdownSelectionService _dropdownSelectionService;

        public DropdownController(DropdownSelectionService dropdownSelectionService)
        {
            _dropdownSelectionService = dropdownSelectionService;
        }

        [HttpPost("getbycategory")]
        public async Task<ActionResult<DropdownDto>> GetDropdownDataByCategory(string category)
        {
            try
            {
                var res = await _dropdownSelectionService.GetDropdownListByCategory(category);

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

        [HttpPost("getbycategorytags")]
        public async Task<ActionResult<DropdownDto>> GetDropdownDataByCategoryTags(string category, string tags)
        {
            try
            {
                var res = await _dropdownSelectionService.GetDropdownListByCategoryTags(category, tags);

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


