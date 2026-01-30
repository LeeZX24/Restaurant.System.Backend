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
        public string OrderNumber { get; set; }
        public string CustomerId { get; set; }
        public string StaffUserName { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public decimal TotalOrderAmount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}