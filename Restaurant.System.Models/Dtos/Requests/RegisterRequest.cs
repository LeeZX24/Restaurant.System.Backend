using Restaurant.System.Models.Dtos.Shared.Base;

namespace Restaurant.System.Models.Dtos.Requests
{
    public class AppRegisterRequest: BaseRequestDto
    {
        public string Identifier { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RegistrarDto RegistrarInfo { get; set; } = new();
        public Guid? CustomerId { get; set; }
        public bool IsEmail { get => Identifier.Contains('@'); }
    }
}