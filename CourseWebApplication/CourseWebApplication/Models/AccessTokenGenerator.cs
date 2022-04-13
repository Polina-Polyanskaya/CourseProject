using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApp.Models
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public AccessTokenGenerator(AuthenticationConfiguration authenticationConfiguration)
        {
            _authenticationConfiguration = authenticationConfiguration;
        }


        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Login)
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationConfiguration.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _authenticationConfiguration.Issuer,
                Audience = _authenticationConfiguration.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMonths(_authenticationConfiguration.AccessTokenExpiration),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return  tokenHandler.WriteToken(token);
        }
    }
}
