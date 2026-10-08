
using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Dtos
{
    public class AccountDto
    {
        public string Identifier { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        public bool IsEmail { get => Identifier.Contains('@') == true; }
    }
}