using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Responses
{
    public class PointResponse
    {
        [Required]
        public Guid Id { get; set; }
    }
}
