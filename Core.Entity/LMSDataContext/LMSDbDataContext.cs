using Core.domain.Models;
using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity.LMSDataContext
{
    public class LMSDbDataContext:DbContext
    {
        public LMSDbDataContext(DbContextOptions options) : base(options)
        {

        }
       
        public DbSet<LoginTb> LoginTb { get; set; }
        public DbSet<DepartmentTb> DepartmentTb { get; set; }
        public DbSet<SesstionTB> SesstionTB { get; set; }
        public DbSet<CoursesTb> CoursesTb { get; set; }
        public DbSet<TeacherTb> TeacherTb { get; set; }
        public DbSet<ClassRoomTb> ClassRoomTb { get; set; }
        public DbSet<AllocationTeacherTb> AllocationTeacherTb { get; set; }
        public DbSet<StudentCourseAllocationTb> StudentCourseAllocationTb { get; set; }
        public DbSet<StudentAttandenceTb> StudentAttandenceTb { get; set; }
        public DbSet<StudentCourseGradesTb> StudentCourseGradesTb { get; set; }

    }
}
