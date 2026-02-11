namespace Restaurant.System.Models.Dtos.Requests
{
    public class LoginRequest
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        // Member
        public string CustomerNumber { get; set; } = string.Empty;
        public bool IsEmail { get => Login.Contains('@'); }
    }
}