using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class ClassRoomTb
    {
        [Key]
        [Column("ClassRoomId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassRoomId { get; set; }
        public String ClassRoom { get; set; }
        public bool IsDeleted { get; set; }
    }
}
