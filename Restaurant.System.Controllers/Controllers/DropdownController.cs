using Microsoft.AspNetCore.Mvc;
using Restaurant.System.Services.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Controllers.Controllers.Base;

namespace Restaurant.System.Controllers.Controllers
{
    [ApiController]
    [Route("dropdown")]
    public class DropdownController : ApiControllerBase
    {
        private readonly IDropdownSelectionService _dropdownSelectionService;

        public DropdownController(IDropdownSelectionService dropdownSelectionService)
        {
            _dropdownSelectionService = dropdownSelectionService;
        }

        [HttpGet("get")]
        public async Task<ActionResult<DropdownDto>> GetDropdownData()
        {
            try
            {
                var res = await _dropdownSelectionService.GetDropdownList();

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


