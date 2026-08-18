using Restaurant.System.Models.Dtos;

namespace Restaurant.System.Services.Interfaces.Maintenance
{
    public interface IDropdownMaintenanceService
    {
        public Task<List<DropdownDto>> GetDropdownListAsync();
        public Task<DropdownDto> AddNewDropdownAsync(DropdownDto dropdownDetails);
        public Task<DropdownDto> UpdateDropdownDetailsAsync(DropdownDto dropdownDetails);
        public Task<DropdownDto> DeleteDropdownAsync(DropdownDto dropdownData);
    }
}