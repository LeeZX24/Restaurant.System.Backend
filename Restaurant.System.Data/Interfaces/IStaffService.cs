using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IStaffService
    {
        Task<List<Staff>> GetStaffList();
        Task<Staff> GetStaffDetail(string username);
        Task AddNewStaff(Staff staff);
        Task DeleteStaff(string username);
        Task UpdateStaff(Staff staff);
    }
}