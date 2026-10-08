using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Dtos
{
    public class UserDto
    {
        public UserType UserType { get; set; }
        public string[] Roles { get; set; } = [];
        public Guid? CustomerId { get; set; }

        public ProfileDto ProfileInfo { get; set; } = new();
        public AccountDto AccountInfo { get; set; } = new();
        public SecurityDto SecurityInfo { get; set; } = new();

        
    }
}