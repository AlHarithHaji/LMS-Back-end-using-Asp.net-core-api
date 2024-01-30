using Core.domain.Models;
using Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Services
{
    public interface ITeacherService
    {
        Task<List<StudentCourseAllocationViewModel>> GetStudentsAttendanceAsync(int CourseId, int DeparmentId);
        Task<string> SaveStudentAttandenceAsync(List<StudentAttandenceTb> valueList);
        Task<string> SaveStudentGradesAsync(List<StudentCourseGradesTb> valueList);
        Task<string> UpgradStudentToNextCourseAsync(List<StudentCourseAllocationTb> valueList);
        Task<List<StudentCourseAllocationViewModel>> GetStudentsGradeAsync(int CourseId, int DeparmentId);
        Task<List<StudentCourseAllocationViewModel>> GetStudentToBeUpgradeAsync(int CourseId, int DeparmentId);
    }
}
