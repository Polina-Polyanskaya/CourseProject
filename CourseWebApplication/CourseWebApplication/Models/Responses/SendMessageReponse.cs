using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SendMessageReponse
    {
        [Required]
        public int Code { get; set; }
    }
}
