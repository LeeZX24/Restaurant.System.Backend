using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class Menu
    {
        public int Id { get; set; } // Primary Key
        public required List<MenuCategory> MenuCategoryList { get; set; } // Foreign Key
        public required string MenuCode { get; set; }
        public required string MenuName { get; set; }
        public MenuDays MenuDays { get; set; } = MenuDays.AllDays;
        public DateTime CreatedDate { get; set; } = DateTime.Now;   
    } 
}