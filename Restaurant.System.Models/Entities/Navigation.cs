namespace Restaurant.System.Models.Entities
{
    public class Navigation
    {
        public int Id { get; set; } // Primary Key
        
        public string Code { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Route { get; set; }
        public string NavigationOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public int Status { get; set; } = 1;

        public Guid NavigationGuid { get; set; }
        public Guid? ParentItemGuid { get; set; }

        public Navigation? ParentItem { get; set; } // Foreign Key, nullable
        public List<Navigation> ChildrenItems { get; set; } = [];

        public List<NavigationRole> NavigationRoleList { get; set; } = [];
    }
}
