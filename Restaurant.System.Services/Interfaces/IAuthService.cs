using Restaurant.System.Models.Dtos.Shared;

namespace Restaurant.System.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<UserDto> LoginAsync(UserDto loginDetails);
        public Task<UserDto> RegisterAsync(UserDto registerDetails);
    }
}