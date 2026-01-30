using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IStaffService
    {
        Task<List<Staff>> GetStaffList();
        Task<Staff> GetStaffDetail(string userName);
    }
}