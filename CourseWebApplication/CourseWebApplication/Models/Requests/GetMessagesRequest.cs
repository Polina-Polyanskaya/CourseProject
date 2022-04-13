using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Requests
{
    public class GetMessagesRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
