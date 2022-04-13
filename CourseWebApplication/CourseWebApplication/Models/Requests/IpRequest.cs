using System.ComponentModel.DataAnnotations;

namespace CourseWebApplication.Models.Requests
{
    public class IpRequest
    {
        [Required]
        public string Ip { get; set; }
    }
}
