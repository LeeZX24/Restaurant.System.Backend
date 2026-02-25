namespace Restaurant.System.Models.Entities
{
    public class Staff
    {
        public int Id { get; set; } // Primary Key
        public Guid StaffId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public required List<StaffRoles> StaffRolesList { get; set; } // Foreign Key
        public List<Order> OrderHistory { get; set; } = new List<Order>(); // Foreign Key
    }
}