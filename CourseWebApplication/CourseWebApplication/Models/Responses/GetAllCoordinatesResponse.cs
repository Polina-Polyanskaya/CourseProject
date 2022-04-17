using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Responses
{
    public class GetAllCoordinatesResponse
    {
        [Required]
        public List<double> Latitude { get; set; }

        [Required]
        public List<double> Longitude { get; set; }

    }
}
