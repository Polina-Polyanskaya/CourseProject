    using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Responses
{
    public class IpResponse
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
    }
}
