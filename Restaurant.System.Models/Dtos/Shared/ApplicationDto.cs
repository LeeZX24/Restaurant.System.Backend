namespace Restaurant.System.Models.Dtos.Shared
{
    public class ApplicationDto : UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public AddressDto Address { get; set; }
    }
}