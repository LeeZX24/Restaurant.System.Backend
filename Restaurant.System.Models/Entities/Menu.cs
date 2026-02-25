namespace Restaurant.System.Models.Entities
{
    public class Menu
    {
        public int Id { get; set; } // Primary Key
        public required string MenuCode { get; set; }
        public required string MenuName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public required List<MenuSection> MenuSections { get; set; } // Foreign Key
    }
}