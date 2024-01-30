using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class AllocationTeacherViewModel
    {
        public int AllocationTeacherId { get; set; }
        public int ClassRoomId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        public DateTime? StartTime { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime? EndTime { get; set; }
        public String ClassRoom { get; set; }
        public String TeacherName { get; set; }
        public String CourseName { get; set; }
    }
}
