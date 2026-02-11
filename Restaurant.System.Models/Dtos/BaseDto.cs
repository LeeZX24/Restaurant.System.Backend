using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Dtos
{
    public class BaseDto
    {
        public RequestDto RequestDetails { get; set; }
        public ResponseDto ResponseDetails { get; set; }
        public Status Status { get; set; } = Status.Pending;
    }
}