namespace Restaurant.System.Models.Entities
{
    public class Member
    {
        public int Id { get; set; } // Primary Key
        public Customer Customer { get; set; } // Foreign Key
        public string MemberId { get; set; }
        public string CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal MemberPoint { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string FullName { get => FirstName + " " + LastName; }
    }
}