using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Repositories
{
    public class StaffRolesService : IStaffRolesService
    {
        private readonly IRepository<StaffRoles> _staffRolesRepository;

        public StaffRolesService(IRepository<StaffRoles> StaffRoleRepository)
        {
            _staffRolesRepository = StaffRoleRepository;
        }

        public async Task<List<StaffRoles>> GetStaffRolesList(string username) => await _staffRolesRepository.GetByFieldAsync(sr => sr.StaffUsername == username);

        public async Task AddStaffRoles(StaffRoles staff)
        {
            await _staffRolesRepository.AddAsync(staff);
            await _staffRolesRepository.SaveChangesAsync();
        }

        public async Task RemoveStaffRoles(StaffRoles staffRoles) => await _staffRolesRepository.DeleteByFieldAsync(sr => (sr.StaffUsername == staffRoles.StaffUsername && sr.RoleCode == staffRoles.RoleCode));

        public async Task RemoveAllStaffRoles(string username) => await _staffRolesRepository.DeleteByFieldAsync(sr => sr.StaffUsername == username);
    }
}