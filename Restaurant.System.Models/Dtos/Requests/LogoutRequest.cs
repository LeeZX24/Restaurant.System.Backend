using Restaurant.System.Models.Dtos.Shared.Base;

namespace Restaurant.System.Models.Dtos.Requests
{
    public class AppLogoutRequest: BaseRequestDto
    {
        public UserDto UserInfo { get; set; } = new();
    }
}