namespace Restaurant.System.Models.Entities
{
    public class Staff
    {
        public int Id { get; set; } // Primary Key
        public List<Role> Roles { get; set; } // Foreign Key
        public List<Order> OrderHistory { get; set; } = new List<Order>(); // Foreign Key
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime LogoutDateTime { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}