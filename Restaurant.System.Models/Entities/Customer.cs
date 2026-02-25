namespace Restaurant.System.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; } // Primary Key
        public Guid CustomerId { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Member MemberDetails { get; set; } // Foreign Key, nullable if is not anonymous
        public List<CustomerOrder> CustomerOrders { get; set; } // Foreign Key
    }
}
