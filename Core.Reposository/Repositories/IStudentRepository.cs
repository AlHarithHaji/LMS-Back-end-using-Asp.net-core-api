using Core.domain.Models;
using Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Reposository.Repositories
{
    public interface IStudentRepository
    {
        Task<LoginViewModel> GetStudentCurrentCourseAsync(int LoginId);
        Task<List<StudentDashbordViewModel>> GetStudentAttendanceAsync(int LoginId);
        Task<List<StudentDashbordViewModel>> GetStudentGradesAsync(int LoginId);
        Task<List<AllocationTeacherViewModel>> GetStudentRoomCourseAndTeacherAllocationAsync(int CourseId);
    }
}
