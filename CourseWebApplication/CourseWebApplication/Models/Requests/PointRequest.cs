using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Requests
{
    public class PointRequest
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
