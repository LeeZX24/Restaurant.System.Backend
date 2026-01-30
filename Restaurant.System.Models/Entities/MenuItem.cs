namespace Restaurant.System.Models.Entities
{
    public class MenuItem
    {
        public int Id { get; set; } // Primary Key
        public required MenuCategory Category { get; set; } // Foreign Key
        public required string MenuItemCode { get; set; }
        public required string MenuItemName { get; set; }
        public required string MenuItemDescription { get; set; }
        public decimal MenuItemPrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}