using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class Order
    {
        public int Id { get; set; } // Primary Key
        public List<OrderItem> OrderItemList { get; set; } // Foreign Key
        public Customer Customer { get; set; } // Foreign Key
        public Staff Staff { get; set; } // Foreign Key
        public Payment Payment { get; set; } // Foreign Key
        public Address DeliveryAddress { get; set; } //Foreign key
        public required string OrderNumber { get; set; }
        public required string CustomerId { get; set; }
        public required OrderType OrderType { get; set; }
        public string StaffUserName { get; set; }
        public required OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public required decimal TotalOrderAmount { get; set; }
        public required string AddressGuid { get; set; }
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}