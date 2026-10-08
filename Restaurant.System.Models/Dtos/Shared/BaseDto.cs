using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Dtos.Shared
{
    public class BaseDto
    {
        public RequestDto RequestDetails { get; set; }
        public ResponseDto ResponseDetails { get; set; }
    }
}