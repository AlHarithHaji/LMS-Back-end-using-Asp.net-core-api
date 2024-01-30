using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.domain.Models
{
    public class LoginTb
    {
        [Key]
        [Column("LoginId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginId { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Mobile { get; set; }
        public String Address { get; set; }
        [Required]
        public String UserType { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int? SessionId { get; set; }
        [Required]
        public String Password { get; set; }
      
        public int? CourseId { get; set; }
       
    }
}
