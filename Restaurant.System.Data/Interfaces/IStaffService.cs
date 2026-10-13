using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IStaffService
    {
        Task<List<Staff>> GetStaffList();
        Task<Staff> GetStaffDetail(string Username);
        Task AddNewStaff(Staff Staff);
        Task DeleteStaff(string Username);
        Task UpdateStaff(Staff Staff);
        Task StaffChangePassword(Guid StaffId, string NewPassword);
    }
}