using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.System.Models.Dtos.Shared
{
    public class UserDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        [NotMapped]
        public string Token { get; set; }
    }
}