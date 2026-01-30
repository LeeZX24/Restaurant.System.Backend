namespace Restaurant.System.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; } // Primary Key
        public Member MemberDetails { get; set; } // Foreign Key, nullable if is not anonymous
        public List<Order> OrderHistory { get; set; } = new List<Order>(); // Foreign Key
        public required string CustomerId { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
