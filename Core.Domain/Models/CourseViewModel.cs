using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class CourseViewModel
    {
        
        public int CourseId { get; set; }
        public int? DeparmentId { get; set; }
        public String? CourseName { get; set; }
        public String? DeparmentName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
