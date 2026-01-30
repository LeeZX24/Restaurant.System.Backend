using Restaurant.System.Data.Interfaces;
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

        public async Task<Staff> GetStaffDetail(string userName)
        {
            var staff = (await _staffRepository.GetByFieldAsync(e => e.UserName == userName)).FirstOrDefault();
            
            return staff;
        }
    }
}