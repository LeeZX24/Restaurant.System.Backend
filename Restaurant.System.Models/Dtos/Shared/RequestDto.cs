using Restaurant.System.Models.Dtos.Shared.Base;

namespace Restaurant.System.Models.Dtos.Shared
{
    public class RequestDto: BaseRequestDto
    {
        public UserDto UserInfo { get; set; } = new();
    }
}