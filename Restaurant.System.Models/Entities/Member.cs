namespace Restaurant.System.Models.Entities
{
    public class Member
    {
        public int Id { get; set; } // Primary Key
        public required Customer Customer { get; set; } // Foreign Key
        public required string MemberId { get; set; }
        public required string CustomerId {get; set;}
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public decimal MemberPoint { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime LogoutDateTime { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

