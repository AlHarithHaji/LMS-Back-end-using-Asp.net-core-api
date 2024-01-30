using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class StudentCourseAllocationTb
    {
        [Key]
        [Column("StudentCourseAllocationId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCourseAllocationId { get; set; }
        public int CourseId { get; set; }
        public int LoginId { get; set; }
        public int SesstionId { get; set; }
        public bool IsCourseActive { get; set; }
    }
}
