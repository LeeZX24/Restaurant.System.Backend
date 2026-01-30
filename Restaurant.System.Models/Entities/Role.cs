namespace Restaurant.System.Models.Entities
{
    public class Role
    {
        public int Id { get; set;}
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public List<Staff> Staffs { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}