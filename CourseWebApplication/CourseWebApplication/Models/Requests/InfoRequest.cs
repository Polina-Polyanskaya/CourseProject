using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Requests
{
    public class InfoRequest
    {
        [Required]
        public string Id { get; set;}

        [Required]
        public string Message { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
