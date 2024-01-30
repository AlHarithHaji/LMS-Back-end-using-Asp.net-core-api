using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class StudentCourseAllocationViewModel
    {
        public int StudentCourseAllocationId { get; set; }
        public int CourseId { get; set; }
        public int LoginId { get; set; }
        public int SesstionId { get; set; }
        public bool IsCourseActive { get; set; }
        public String? CourseName { get; set; }
        public String? SesstionName { get; set; }
        public String? StudentName { get; set; }
        public String? Email { get; set; }
        public double? Presents { get; set; }
        public double? TotalLectures { get; set; }
        public int? DepartmentId { get; set; }
        public Double? TotalMarks { get; set; }
        public double? ObtainMarks { get; set; }
    }
}
