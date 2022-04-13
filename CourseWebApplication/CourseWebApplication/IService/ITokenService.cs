using WebApp.Models.Requests;

namespace WebApp.Jwt
{
    public interface ITokenService
    {
        Task<LoginResponse> Authenticate(LoginRequest userDto);
    }
}
