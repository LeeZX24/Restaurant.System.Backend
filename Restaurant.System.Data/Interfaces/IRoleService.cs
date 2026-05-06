using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetRoleList();
        Task<Role> GetRoleDetail(string roleCode);
        Task AddNewRole(Role role);
        Task DeleteRole(string roleCode);
        Task UpdateRole(Role role);
    }
}