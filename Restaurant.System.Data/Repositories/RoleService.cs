using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Repositories
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<Role>> GetRoleList() => await _roleRepository.GetAllAsync();

        public async Task<Role> GetRoleDetail(string roleCode)
        {
            var role = (await _roleRepository.GetByFieldAsync(e => e.RoleCode == roleCode)).FirstOrDefault();

            return role;
        }

        public async Task AddNewRole(Role role)
        {
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
        }
        public async Task UpdateRole(Role role)
        {
            await _roleRepository.UpdateAsync(role);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task DeleteRole(string roleCode) => await _roleRepository.DeleteByFieldAsync(s => s.RoleCode == roleCode);
    }
}