namespace Restaurant.System.Models.Entities
{
    public class Member
    {
        public int Id { get; set; } // Primary Key
        public Guid MemberId { get; set; } = Guid.CreateVersion7();
        public Guid CustomerId { get; set; }
        public string Username { get; set; }
        public required string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal MemberPoint { get; set; }
        public List<Address> AddressList { get; set; } // Foreign Key
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string FullName { get => $"{FirstName ?? string.Empty} {LastName ?? string.Empty}".Trim(); }
        public Customer Customer { get; set; } // Foreign Key
    }
}