using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SendMessageRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
