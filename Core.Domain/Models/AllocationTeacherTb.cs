using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class AllocationTeacherTb
    {
        [Key]
        [Column("AllocationTeacherId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AllocationTeacherId { get; set; }
        public int ClassRoomId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        public DateTime? StartTime { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime? EndTime { get; set; }
    }
}
