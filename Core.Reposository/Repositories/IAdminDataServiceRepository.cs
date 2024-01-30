

using Core.domain.Models;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Reposository.Repositories
{
    public interface IAdminDataServiceRepository
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
        Task<bool> CheckClassRoomAvailableAsync(AllocationTeacherTb allocationTeacher);
        Task<bool> CheckTeachAvailableAsync(AllocationTeacherTb allocationTeacher);

        //List<LoginTb> GetAllUsers();
        //List<DepartmentTb> GetDeparmentsList();
        //String RegisterUser(LoginViewModel value);
        //LoginViewModel LoginUser(UserLoginModel value);
        //List<LoginViewModel> GetNewRegisterUser();
        //String ActiveUser(int LoginId);
        //SesstionTB GetActiveSession();
        //List<LoginViewModel> GetExcelOfRegisterStudent(int DeparmentId);
        //String SendEmailToRegisterStudent(int DeparmentId);
        //List<TeacherTb> GetAllTeachers();
        //String SaveTeacher(TeacherTb value);
        //string DeleteTeacher(int TeacherId);
        //List<CourseViewModel> GetAllCourse();
        //String SaveCourse(CourseViewModel value);
        //string DeleteCourse(int CourseId);
        //List<ClassRoomTb> GetAllClassRoom();
        //String SaveClassRoom(ClassRoomTb value);
        //string DeleteClassRoom(int ClassRoomId);
        //List<LoginTb> GetAdminAndTeacher();
        //String RoomAndTeacherAllocation(AllocationTeacherTb value);
        //List<AllocationTeacherViewModel> GetRoomAndTeacherAllocation();
        //String DeleteRoomAndTeacherAllocation(int id);
        //String DeActiveUser(int LoginId);
        //List<LoginViewModel> GetAllStudent(int DeparmentId);
    }
}
