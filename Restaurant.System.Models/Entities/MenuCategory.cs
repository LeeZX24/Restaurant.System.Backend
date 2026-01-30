namespace Restaurant.System.Models.Entities
{
    public class MenuCategory
    {
        public int Id { get; set; } // Primary Key
        public required List<MenuItem> MenuItemList { get; set; } // Foreign Key
        public required List<Menu> MenuList { get; set; } // Foreign Key
        public required string MenuCategoryCode { get; set; }
        public required string MenuCategoryName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}