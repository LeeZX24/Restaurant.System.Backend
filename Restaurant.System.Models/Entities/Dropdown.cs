namespace Restaurant.System.Models.Entities
{
    public class Dropdown
    {
        public int Id { get; set; } // Primary Key
        public string Category { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public string Tags { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public int Status { get; set; } = 1;
    }
}