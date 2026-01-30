using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Dtos.Shared;

namespace Restaurant.System.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<UserDto> LoginAsync(UserDto user);
        public Task<UserDto> RegisterAsync(CustomerDto customer);
    }
}