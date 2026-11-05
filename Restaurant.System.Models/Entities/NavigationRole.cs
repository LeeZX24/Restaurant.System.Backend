namespace Restaurant.System.Models.Entities
{
    public class NavigationRole
    {
        public int Id { get; set; } // Primary Key

        public string RoleCode { get; set; }
        public Guid NavigationGuid { get; set; }

        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }

        public Navigation Navigation { get; set; } // Foreign Key
        public Role Role { get; set; } // Foreign Key
    }
}
