using Core.domain.Models;
using Core.Domain.Models;
using Core.Reposository.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRespository _teacherRespository;

        public TeacherService(ITeacherRespository teacherRespository)
        {
            _teacherRespository = teacherRespository;
        }

        public Task<List<StudentCourseAllocationViewModel>> GetStudentsAttendanceAsync(int CourseId, int DeparmentId)
        {
            return _teacherRespository.GetStudentsAttendanceAsync(CourseId, DeparmentId);
        }

        public Task<string> SaveStudentAttandenceAsync(List<StudentAttandenceTb> valueList)
        {
            return _teacherRespository.SaveStudentAttandenceAsync(valueList);
        }

        public Task<string> SaveStudentGradesAsync(List<StudentCourseGradesTb> valueList)
        {
            return _teacherRespository.SaveStudentGradesAsync(valueList);
        }

        public Task<string> UpgradStudentToNextCourseAsync(List<StudentCourseAllocationTb> valueList)
        {
            return _teacherRespository.UpgradStudentToNextCourseAsync(valueList);
        }

        public Task<List<StudentCourseAllocationViewModel>> GetStudentsGradeAsync(int CourseId, int DeparmentId)
        {
            return _teacherRespository.GetStudentsGradeAsync(CourseId, DeparmentId);
        }

        public Task<List<StudentCourseAllocationViewModel>> GetStudentToBeUpgradeAsync(int CourseId, int DeparmentId)
        {
            return _teacherRespository.GetStudentToBeUpgradeAsync(CourseId, DeparmentId);
        }
    }
}
