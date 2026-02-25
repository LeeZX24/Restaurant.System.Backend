using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; } // Primary Key
        public Guid OrderGroupId { get; set; }
        public required string MenuItemCode { get; set; }
        public OrderItemStatus OrderItemStatus { get; set; } = OrderItemStatus.Draft;
        public int Quantity { get; set; }
        public decimal OrderPrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public OrderGroup OrderGroup { get; set; } // Foreign Key
        public MenuItem MenuItem { get; set; } //Foreign Key
    }
}