
using Core.domain.Models;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Services
{
    public interface IAdminService
    {
        Task<List<LoginTb>> GetAllUsersAsync();
        Task<List<DepartmentTb>> GetDepartmentsListAsync();
        Task<string> RegisterUserAsync(LoginViewModel value);
        Task<LoginViewModel> LoginUserAsync(UserLoginModel value);
        Task<List<LoginViewModel>> GetNewRegisterUserAsync();
        Task<string> ActiveUserAsync(int LoginId);
        Task<SesstionTB> GetActiveSessionAsync();
        Task<List<LoginViewModel>> GetExcelOfRegisterStudentAsync(int DepartmentId);
        Task<string> SendEmailToRegisterStudentAsync(int DepartmentId);
        Task<List<TeacherTb>> GetAllTeachersAsync();
        Task<string> SaveTeacherAsync(TeacherTb value);
        Task<string> DeleteTeacherAsync(int TeacherId);
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<string> SaveCourseAsync(CourseViewModel value);
        Task<string> DeleteCourseAsync(int CourseId);
        Task<List<ClassRoomTb>> GetAllClassRoomAsync();
        Task<string> SaveClassRoomAsync(ClassRoomTb value);
        Task<string> DeleteClassRoomAsync(int ClassRoomId);
        Task<List<LoginTb>> GetAdminAndTeacherAsync();
        Task<string> RoomAndTeacherAllocationAsync(AllocationTeacherTb value);
        Task<List<AllocationTeacherViewModel>> GetRoomAndTeacherAllocationAsync();
        Task<string> DeleteRoomAndTeacherAllocationAsync(int id);
        Task<string> DeActiveUserAsync(int LoginId);
        Task<List<LoginViewModel>> GetAllStudentAsync(int DepartmentId);
    }
 

}
