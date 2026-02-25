using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class MenuSchedule
    {
        public int Id { get; set; } // Primary Key
        public required string ScheduleCode { get; set; }
        public MenuDays MenuDays { get; set; } = MenuDays.AllDays;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<MenuSection> MenuSectionList { get; set; } // Foreign Key
    }
}