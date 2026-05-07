namespace Restaurant.System.Models.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<StaffRoles> StaffRolesList { get; set; } // Foreign Key
    }
}