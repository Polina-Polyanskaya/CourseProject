using System.ComponentModel.DataAnnotations;

namespace WebApp.Jwt
{
    public class LoginResponse
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
