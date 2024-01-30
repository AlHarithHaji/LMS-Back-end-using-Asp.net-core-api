using System.Collections.Generic;
using System.Threading.Tasks;
using Core.domain.Models;
using Core.Domain.Models;

namespace Core.Services.Services
{
    public interface IStudentService
    {
        Task<List<StudentDashbordViewModel>> GetStudentAttendanceAsync(int LoginId);
        Task<List<StudentDashbordViewModel>> GetStudentGradesAsync(int LoginId);
        Task<List<AllocationTeacherViewModel>> GetStudentRoomCourseAndTeacherAllocationAsync(int CourseId);
        Task<LoginViewModel> GetStudentCurrentCourseAsync(int LoginId);
    }
}
