using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class StudentAttandenceTb
    {
        [Key]
        [Column("StudentAttanenceId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentAttanenceId { get; set; }
        public int DeparmentId { get; set; }
        public int CourseId { get; set; }
        public int LoginId { get; set; }
        public int SessionId { get; set; }
        public double Presents { get; set;}
        public double TotalLectures { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
