using Restaurant.System.Models.Entities;
using Restaurant.System.Models.Enums;

namespace Restaurant.System.Data.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUserList();
        Task<DateTime> GetUserLastLogin(string userId);
        Task<User> GetUserSession(string userId);
        Task UpdateUserLogin(string userId);
        Task UpdateUserLogout(string userId);
        Task<User> AddUserSession(string userId, UserType userType);
    }
}