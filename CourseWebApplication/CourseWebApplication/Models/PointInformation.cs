using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWebApplication.Models
{
    public class PointInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid PointId { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }

        public Point Point { get; set; }

    }
}
