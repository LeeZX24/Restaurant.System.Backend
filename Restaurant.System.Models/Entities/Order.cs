using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class Order
    {
        public int Id { get; set; } // Primary Key
        public required string OrderNumber { get; set; }
        public string StaffUsername { get; set; }
        public required string PaymentId { get; set; }
        public Guid AddressId { get; set; }
        public required OrderType OrderType { get; set; }
        public required OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public required decimal TotalOrderAmount { get; set; }
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<OrderGroup> OrderGroups { get; set; } // Foreign Key
        public List<CustomerOrder> CustomerOrders { get; set; } // Foreign Key
        public Staff Staff { get; set; } // Foreign Key
        public Payment Payment { get; set; } // Foreign Key
        public OrderAddress DeliveryAddress { get; set; } //Foreign key
    }
}