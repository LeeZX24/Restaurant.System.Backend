using Restaurant.System.Models.Dtos.Shared.Base;

namespace Restaurant.System.Models.Dtos.Requests
{
    public class AppLoginRequest: BaseRequestDto
    {
        public string Identifier { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsEmail { get => Identifier.Contains('@') == true; }
    }
}