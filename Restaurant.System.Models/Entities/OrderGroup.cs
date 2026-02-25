using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class OrderGroup
    {
        public int Id { get; set; } // Primary Key
        public Guid OrderGroupId { get; set; } = Guid.CreateVersion7();
        public required string OrderNumber { get; set; }
        public required string GroupName { get; set; }
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> OrderItemList { get; set; } // Foreign Key
        public Order Order { get; set; } // Foreign Key
    }
}