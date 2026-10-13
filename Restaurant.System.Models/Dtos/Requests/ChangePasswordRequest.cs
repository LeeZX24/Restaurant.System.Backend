using Restaurant.System.Models.Dtos.Shared.Base;

namespace Restaurant.System.Models.Dtos.Requests
{
    public class AppChangePasswordRequest: BaseRequestDto
    {
        public UserDto UserInfo { get; set; } = new();
        public string NewPassword { get; set; } = string.Empty;
    }
}