using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Entities;
using Restaurant.System.Models.Enums;

namespace Restaurant.System.Data.Repositories
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> UserRepository)
        {
            _userRepository = UserRepository;
        }

        public async Task<User> AddUserSession(string userId, UserType userType)
        {
            var newUserSession = new User
            {
                UserId = userId,
                UserType = userType
            };

            await _userRepository.AddAsync(newUserSession);
            await _userRepository.SaveChangesAsync();

            return newUserSession;
        }

        public async Task<User> GetUserSession(string userId)
        {
            return (await _userRepository.GetByFieldAsync(user => user.UserId == userId)).FirstOrDefault();
        }

        public async Task<DateTime> GetUserLastLogin(string userId)
        {
            return (await _userRepository.GetByFieldAsync(user => user.UserId == userId)).FirstOrDefault().LoginDateTime;
        }

        public async Task<List<User>> GetUserList() => await _userRepository.GetAllAsync();

        public async Task UpdateUserLogin(string userId)
        {
            await _userRepository.UpdateByFieldAsync(
                user => user.UserId == userId,
                user => user.LoginDateTime,
                user => DateTime.UtcNow
            );

            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateUserLogout(string userId)
        {
            await _userRepository.UpdateByFieldAsync(
                user => user.UserId == userId,
                user => user.LogoutDateTime,
                user => DateTime.UtcNow
            );

            await _userRepository.SaveChangesAsync();
        }
    }
}