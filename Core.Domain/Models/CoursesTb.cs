using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.domain.Models
{
    public class CoursesTb
    {
        [Key]
        [Column("CourseId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public int? DeparmentId { get; set; }
        public String? CourseName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
