using System.Collections.Generic;
using System.Threading.Tasks;
using Core.domain.Models;
using Core.Domain.Models;
using Core.Reposository.Repositories;

namespace Core.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Task<List<StudentDashbordViewModel>> GetStudentAttendanceAsync(int LoginId)
        {
            return _studentRepository.GetStudentAttendanceAsync(LoginId);
        }

        public Task<List<StudentDashbordViewModel>> GetStudentGradesAsync(int LoginId)
        {
            return _studentRepository.GetStudentGradesAsync(LoginId);
        }

        public Task<List<AllocationTeacherViewModel>> GetStudentRoomCourseAndTeacherAllocationAsync(int CourseId)
        {
            return _studentRepository.GetStudentRoomCourseAndTeacherAllocationAsync(CourseId);
        }

        public Task<LoginViewModel> GetStudentCurrentCourseAsync(int LoginId)
        {
            return _studentRepository.GetStudentCurrentCourseAsync(LoginId);
        }
    }
}
