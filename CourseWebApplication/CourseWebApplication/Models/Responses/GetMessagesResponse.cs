using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Responses
{
    public class GetMessagesResponse
    {
        [Required]
        public List<string> Messages { get; set; }
    }
}
