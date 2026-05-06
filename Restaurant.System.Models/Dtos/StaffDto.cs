using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Models.Dtos
{
    public class StaffDto : ApplicationDto
    {
        public Role[] RoleList { get; set; }
    }
}