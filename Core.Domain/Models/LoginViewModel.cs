using System.ComponentModel.DataAnnotations;

namespace Core.domain.Models
{
    public class LoginViewModel
    {
        public int? LoginId { get; set; }
      
        public String FirstName { get; set; }
       
        public String LastName { get; set; }
     
        public String Email { get; set; }
     
        public String Mobile { get; set; }
        public String Address { get; set; }
      
        public String UserType { get; set; }
     
        public bool? IsActive { get; set; }
     
        public int DepartmentId { get; set; }
       
        public String Password { get; set; }
        public String? DepartName { get; set; }
        public String? CourseName { get; set; }
        public int? SessionId { get; set; }
        public String? Token { get; set; }
        public int? CourseId { get; set; }
    }
}
