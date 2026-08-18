using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;
using Restaurant.System.Models.Enums;
using Restaurant.System.Services.Interfaces.Maintenance;

namespace Restaurant.System.Services.Services.Maintenance
{
    public class DropdownMaintenanceService : IDropdownMaintenanceService
    {
        private readonly IDropdownService _dropdownService;

        public DropdownMaintenanceService(
            IDropdownService dropdownService
        )
        {
            _dropdownService = dropdownService;
        }
        public async Task<List<DropdownDto>> GetDropdownListAsync()
        {
            List<DropdownDto> DropdownDataList = new List<DropdownDto>();

            var DropdownList = await _dropdownService.GetDropdownList();

            foreach (Dropdown dropdown in DropdownList)
            {
                DropdownDataList.Add(new DropdownDto
                {
                    Category = dropdown.Category,
                    Code = dropdown.Code,
                    Description = dropdown.Description,
                    SeqNo = dropdown.Sequence,
                    Tags = dropdown.Tags
                });
            }

            return DropdownDataList;
        }

        public async Task<DropdownDto> AddNewDropdownAsync(DropdownDto dropdownData)
        {
            var dropdown = await _dropdownService.GetDropdownDetails(dropdownData);
            if (dropdown != null) throw new UnauthorizedAccessException("Dropdown Already Existed.");
            else
            {
                dropdown = new Dropdown
                {
                    Category = dropdownData.Category,
                    Code = dropdownData.Code,
                    Description = dropdownData.Description,
                    Sequence = dropdownData.SeqNo,
                    Tags = dropdownData.Tags
                };

                await _dropdownService.CreateNewDropdown(dropdown);
            }

            dropdownData.Status = Status.Success;
            dropdownData.ResponseDetails = new ResponseDto
            {
                Message = "Dropdown Adding Success."
            };

            return dropdownData;
        }

        public async Task<DropdownDto> UpdateDropdownDetailsAsync(DropdownDto dropdownData)
        {
            var dropdown = await _dropdownService.GetDropdownDetails(dropdownData);

            dropdown.Category = dropdownData.Category;
            dropdown.Code = dropdownData.Code;
            dropdown.Description = dropdownData.Description;
            dropdown.Sequence = dropdownData.SeqNo;
            dropdown.Tags = dropdownData.Tags;

            await _dropdownService.UpdateExistingDropdown(dropdown);

            dropdownData.Status = Status.Success;
            dropdownData.ResponseDetails = new ResponseDto
            {
                Message = "Dropdown Updated Success."
            };

            return dropdownData;
        }

        public async Task<DropdownDto> DeleteDropdownAsync(DropdownDto dropdownData)
        {
            var dropdown = await _dropdownService.GetDropdownDetails(dropdownData);
            if (dropdown != null) await _dropdownService.DeleteDropdown(dropdown);

            dropdownData.Status = Status.Success;
            dropdownData.ResponseDetails = new ResponseDto
            {
                Message = "Dropdown Delete Success."
            };

            return dropdownData;
        }
    }
}