using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public UserType UserType { get; set; }
        public Guid UserId { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime LogoutDateTime { get; set; }
    }
}