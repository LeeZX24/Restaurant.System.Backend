using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Repositories
{
    public class StaffService : IStaffService
    {
        private readonly IRepository<Staff> _staffRepository;

        public StaffService(IRepository<Staff> StaffRepository)
        {
            _staffRepository = StaffRepository;
        }

        public async Task<List<Staff>> GetStaffList() => await _staffRepository.GetAllAsync();

        public async Task<Staff> GetStaffDetail(string username)
        {
            var staff = (await _staffRepository.GetByFieldAsync(e => e.Username == username)).FirstOrDefault();

            return staff;
        }

        public async Task AddNewStaff(Staff staff)
        {
            await _staffRepository.AddAsync(staff);
            await _staffRepository.SaveChangesAsync();
        }
        public async Task UpdateStaff(Staff staff)
        {
            await _staffRepository.UpdateAsync(staff);
            await _staffRepository.SaveChangesAsync();
        }

        public async Task DeleteStaff(string username) => await _staffRepository.DeleteByFieldAsync(s => s.Username == username);
    }
}