using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.domain.Models
{
    public class TeacherTb
    {
        [Key]
        [Column("TeacherId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        public String? TeacherName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
