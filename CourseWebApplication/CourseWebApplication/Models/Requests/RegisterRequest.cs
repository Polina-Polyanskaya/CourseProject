using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
