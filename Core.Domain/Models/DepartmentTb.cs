using System.ComponentModel.DataAnnotations;

namespace Core.domain.Models
{
    public class DepartmentTb
    {
        [Key]
        public int DepartmentId { get; set; }
        public String? DeparmentName { get; set; }
        public bool? IsPaiad { get; set; }
    }
}
