using Restaurant.System.Models.Dtos.Requests;
using Restaurant.System.Models.Dtos.Responses;

namespace Restaurant.System.Services.Interfaces.Auth
{
    public interface IAuthService
    {
        public Task<AppLoginResponse> LoginAsync(AppLoginRequest loginDetails);

        public Task<AppRegisterResponse> RegisterAsync(AppRegisterRequest registerDetails);

        public Task<AppLogoutResponse> LogoutAsync(AppLogoutRequest logoutDetails);
    }
}