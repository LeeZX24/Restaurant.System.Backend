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

        public async Task<Staff> GetStaffDetail(string Username)
        {
            var staff = (await _staffRepository.GetByFieldAsync(e => e.Username == Username)).FirstOrDefault();

            return staff;
        }

        public async Task AddNewStaff(Staff Staff)
        {
            await _staffRepository.AddAsync(Staff);
            await _staffRepository.SaveChangesAsync();
        }
        public async Task UpdateStaff(Staff Staff)
        {
            await _staffRepository.UpdateAsync(Staff);
            await _staffRepository.SaveChangesAsync();
        }

        public async Task StaffChangePassword(Guid StaffId, string NewPassword)
        {
            await _staffRepository.UpdateByFieldAsync(staff => 
                staff.StaffId == StaffId,
                staff => staff.Password, 
                staff => NewPassword);
            await _staffRepository.SaveChangesAsync();
        }

        public async Task DeleteStaff(string Username) => await _staffRepository.DeleteByFieldAsync(s => s.Username == Username);
    }
}