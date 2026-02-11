using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Dtos.Shared
{
    public class UserDto : BaseDto
    {
        public string Identifier { get; set; }
        public UserType UserType { get; set; }
        public string Password { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime? ExpireAt { get; set; }
        //Staff
        public string[] Roles { get; set; } = [];
        public string CustomerId { get; set; }
        public bool IsEmail { get => Identifier.Contains('@'); }
    }
}