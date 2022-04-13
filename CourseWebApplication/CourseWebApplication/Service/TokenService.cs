using WebApp.Models;
using WebApp.Models.Requests;

namespace WebApp.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly AccessTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;

        public TokenService(IUserRepository userRepository, AccessTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetUser(loginRequest);

            if (user == null)
                return null;

            return new LoginResponse { AccessToken=_tokenGenerator.CreateToken(user) };
        }
    }
}
