using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class StudentCourseGradesTb
    {
        [Key]
        [Column("StudentCourseGradesId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCourseGradesId { get; set; }
        public int LoginId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get;set; }
        public int SessionId { get; set; }
        public int DeparmentId { get; set; }
        public Double TotalMarks { get; set; }
        public double ObtainMarks { get; set; }
        public double Percentage { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
