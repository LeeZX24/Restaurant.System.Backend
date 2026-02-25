using Restaurant.System.Models.Entities;
using Restaurant.System.Models.Enums;

namespace Restaurant.System.Data.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUserList();
        Task<DateTime> GetUserLastLogin(Guid userId);
        Task<User> GetUserSession(Guid userId);
        Task UpdateUserLogin(Guid userId);
        Task UpdateUserLogout(Guid userId);
        Task<User> AddUserSession(Guid userId, UserType userType);
    }
}