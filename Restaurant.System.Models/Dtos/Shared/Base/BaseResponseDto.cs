using Restaurant.System.Models.Enums;

namespace Restaurant.System.Models.Dtos.Shared.Base
{
    public class BaseResponseDto
    {
        public string Message { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public bool IsSuccess => Status == Status.Success;
        public bool IsFailed => Status == Status.Failed;
    }
}