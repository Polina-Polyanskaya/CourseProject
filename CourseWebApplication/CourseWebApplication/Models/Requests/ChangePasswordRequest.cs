using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Password { get; set; }
    }
}
