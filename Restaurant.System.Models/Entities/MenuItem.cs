namespace Restaurant.System.Models.Entities
{
    public class MenuItem
    {
        public int Id { get; set; } // Primary Key
        public required string ItemCode { get; set; }
        public required string ItemName { get; set; }
        public required string ItemDescription { get; set; }
        public decimal ItemPrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public required MenuCategory Category { get; set; } // Foreign Key
    }
}