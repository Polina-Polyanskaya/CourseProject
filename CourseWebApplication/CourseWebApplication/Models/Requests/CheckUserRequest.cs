using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class CheckUserRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
