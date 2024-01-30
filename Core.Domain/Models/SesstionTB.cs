using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.domain.Models
{
    public class SesstionTB
    {
        [Key]
        [Column("SesstionId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SesstionId { get; set; }
        public string? Session { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
