using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required UserType UserType { get; set; }
        public required string UserId { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime LogoutDateTime { get; set; }
    }
}