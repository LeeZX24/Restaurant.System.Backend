namespace Restaurant.System.Models.Entities
{
    public class MenuCategory
    {
        public int Id { get; set; } // Primary Key
        public required string CategoryCode { get; set; }
        public required string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public required List<MenuItem> MenuItemList { get; set; } // Foreign Key
        public List<MenuSection> MenuSections { get; set; } // Foreign Key
    }
}