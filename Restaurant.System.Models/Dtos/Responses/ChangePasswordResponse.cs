using Restaurant.System.Models.Dtos.Shared.Base;

namespace Restaurant.System.Models.Dtos.Responses
{
    public class AppChangePasswordResponse: BaseResponseDto
    {
        public bool IsPasswordChanged { get; set; }
    }
}