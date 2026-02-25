namespace Restaurant.System.Models.Entities
{
    public class StaffRoles
    {
        public int Id { get; set; } // Primary Key
        public required string StaffUsername { get; set; }
        public required string RoleCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Staff Staff { get; set; } // Foreign Key
        public Role Role { get; set; } // Foreign Key
    }
}