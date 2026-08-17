namespace Restaurant.System.Models.Entities
{
    public class SystemConfig
    {
        public int Id { get; set; } // Primary Key
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public int Status { get; set; } = 1;
    }
}