
namespace Restaurant.System.Models.Dtos
{
    public class DropdownDto
    {
        public string CategoryDD { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SeqNo { get; set; }
        public string Tags { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
}