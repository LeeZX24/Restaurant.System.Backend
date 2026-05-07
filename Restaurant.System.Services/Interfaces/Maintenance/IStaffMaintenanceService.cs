using Restaurant.System.Models.Dtos;

namespace Restaurant.System.Services.Interfaces
{
    public interface IStaffMaintenanceService
    {
        public Task<List<StaffDto>> GetStaffListAsync();
        public Task<StaffDto> GetStaffDetailsAsync(string username);
        public Task<StaffDto> AddNewStaffAsync(StaffDto staffDetails);
        public Task<StaffDto> UpdateStaffDetailsAsync(StaffDto staffDetails);
        public Task<StaffDto> DeleteStaffAsync(string username);
    }
}