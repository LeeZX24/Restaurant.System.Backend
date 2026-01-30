namespace Restaurant.System.Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; } // Primary Key
        public Order Order { get; set; } // Foreign Key
        public MenuItem MenuItem { get; set; } //Foreign Key
        public string OrderNumber { get; set; }
        public string MenuItemCode { get; set; }
        public int Quantity { get; set; }
        public decimal OrderPrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;   
    }
}