using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Responses
{
    public class NearCoordResponse
    {
        [Required]
        public List<double> FoundCoord { get; set; }
    }
}
