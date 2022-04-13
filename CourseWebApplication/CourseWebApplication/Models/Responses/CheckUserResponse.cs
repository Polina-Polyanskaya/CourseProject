using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.folder
{
    public class CheckUserResponse
    {
        [Required]
        public bool Exists { get; set; }
    }
}
