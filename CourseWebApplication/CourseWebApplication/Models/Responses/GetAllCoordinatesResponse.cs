using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Responses
{
    public class GetAllCoordinatesResponse
    {
        [Required]
        public List<(double, double)> Coordinates { get; set; }
    }
}
