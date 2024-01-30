using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class StudentDashbordViewModel
    {
        public String? CourseName { get; set; }
        public String? Session { get; set; }
        public double? ObtainMarks { get; set; }
        public double? TotalMarks { get; set; }
        public double? Presence { get; set; }
        public double? TotalLectures { get; set; }
        public double? PercenTage { get; set; }
    }
}
