namespace Restaurant.System.Models.Dtos
{
    public class ProfileDto
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string ProfilePicUrl { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new();
    }
}