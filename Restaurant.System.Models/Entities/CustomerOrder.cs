namespace Restaurant.System.Models.Entities
{
    public class CustomerOrder
    {
        public int Id { get; set; } // Primary Key
        public Guid OrderSession { get; set; } = Guid.CreateVersion7();
        public Guid CustomerId { get; set; }
        public required string OrderNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Customer Customer { get; set; } // Foreign Key
        public Order Order { get; set; } // Foreign Key
    }
}
