using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IStaffRolesService
    {
        Task<List<StaffRoles>> GetStaffRolesList(string username);
        Task AddStaffRoles(StaffRoles staffRoles);
        Task RemoveStaffRoles(StaffRoles staffRoles);
        Task RemoveAllStaffRoles(string username);
    }
}