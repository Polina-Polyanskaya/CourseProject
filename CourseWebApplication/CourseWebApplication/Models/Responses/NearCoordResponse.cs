using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Responses
{
    public class NearCoordResponse
    {
        [Required]
        public (double, double)? FoundCoord { get; set; }
    }
}
