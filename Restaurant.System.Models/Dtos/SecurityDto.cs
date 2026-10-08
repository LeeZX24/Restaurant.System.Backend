namespace Restaurant.System.Models.Dtos
{
    public class SecurityDto
    {
        public bool IsEmailVerified { get; set; }
        public bool IsMfaEnabled { get; set; }
    }
}