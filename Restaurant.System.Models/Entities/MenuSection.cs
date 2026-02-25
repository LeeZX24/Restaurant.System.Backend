using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class MenuSection
    {
        public int Id { get; set; } // Primary Key
        public required string SectionCode { get; set; }
        public required string MenuCode { get; set; }
        public required string CategoryCode { get; set; }
        public required string ScheduleCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Menu Menu { get; set; } // Foreign Key
        public MenuCategory Category { get; set; } // Foreign Key
        public MenuSchedule MenuSchedule { get; set; } // Foreign Key
    }
}