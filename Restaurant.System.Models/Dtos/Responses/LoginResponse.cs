using Restaurant.System.Models.Dtos.Shared.Base;

namespace Restaurant.System.Models.Dtos.Responses
{
    public class AppLoginResponse: BaseResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime? ExpireAt { get; set; }

        public UserDto UserInfo { get; set; } = new();
    }
}