using Restaurant.System.Models.Dtos.Shared;

namespace Restaurant.System.Models.Dtos
{
    public class StaffDto: BaseDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public UserDto UserInfo { get; set; } = new();
    }
}