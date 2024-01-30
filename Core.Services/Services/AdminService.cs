
//using Core.domain.Models;
//using Core.Domain.Models;
//using Core.Reposository.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Core.Services.Services
//{
//    public class AdminService: IAdminService
//    {
//        private readonly IAdminDataServiceRepository _adminDataServiceRepository;
//        public AdminService(IAdminDataServiceRepository adminDataServiceRepository) {
//            this._adminDataServiceRepository = adminDataServiceRepository;
//        }
//        public async Task<List<LoginTb>> GetAllUsersAsync()
//        {
//            return await _adminDataServiceRepository.GetAllUsersAsync();
//        }
//        public async Task <List<DepartmentTb>> GetDepartmentsListAsync() {
//            return await  _adminDataServiceRepository.GetDeparmentsListAsync();
//        }
//        public String RegisterUser(LoginViewModel value) { 
//            return _adminDataServiceRepository.RegisterUser(value);
//        }
//        public LoginViewModel LoginUser(UserLoginModel value) {
//            return _adminDataServiceRepository.LoginUser(value);
//        }
//        public List<LoginViewModel> GetNewRegisterUser() { 
//        return (_adminDataServiceRepository.GetNewRegisterUser());
//        }
//       public String ActiveUser(int LoginId) {
//            return _adminDataServiceRepository.ActiveUser(LoginId);
//        }
//        public SesstionTB GetActiveSession() { 
//            return _adminDataServiceRepository.GetActiveSession();
//        }
//        public List<LoginViewModel> GetExcelOfRegisterStudent(int DeparmentId) {
//            return _adminDataServiceRepository.GetExcelOfRegisterStudent( DeparmentId);
//        }
//        public String SendEmailToRegisterStudent(int DeparmentId)
//        {
//            return _adminDataServiceRepository.SendEmailToRegisterStudent( DeparmentId);
//        }
//        public List<TeacherTb> GetAllTeachers() {
//            return _adminDataServiceRepository.GetAllTeachers();
//        }
//        public String SaveTeacher(TeacherTb value)
//        { 
//        return _adminDataServiceRepository.SaveTeacher(value);
//        }
//        public string DeleteTeacher(int TeacherId) {
//            return _adminDataServiceRepository.DeleteTeacher(TeacherId);
//        }
//        public List<CourseViewModel> GetAllCourse() { 
//        return _adminDataServiceRepository.GetAllCourse();
//        }
//        public String SaveCourse(CourseViewModel value)
//        {
//            return _adminDataServiceRepository.SaveCourse(value);
//        }
//        public string DeleteCourse(int CourseId) { 
//            return _adminDataServiceRepository.DeleteCourse(CourseId);
//        }
//        public List<ClassRoomTb> GetAllClassRoom() { 
//            return _adminDataServiceRepository.GetAllClassRoom();   
//        }
//        public String SaveClassRoom(ClassRoomTb value) { 
//        return _adminDataServiceRepository.SaveClassRoom(value);    
//        }
//        public string DeleteClassRoom(int ClassRoomId) {
//            return _adminDataServiceRepository.DeleteClassRoom(ClassRoomId);
//        }
//        public List<LoginTb> GetAdminAndTeacher()
//        {
//            return _adminDataServiceRepository.GetAdminAndTeacher();
//        }
//        public String RoomAndTeacherAllocation(AllocationTeacherTb value)
//        {
//            return _adminDataServiceRepository.RoomAndTeacherAllocation(value);
//        }
//        public List<AllocationTeacherViewModel> GetRoomAndTeacherAllocation() 
//        { 
//            return _adminDataServiceRepository.GetRoomAndTeacherAllocation();
//        }
//        public String DeleteRoomAndTeacherAllocation(int id) {
//            return _adminDataServiceRepository.DeleteRoomAndTeacherAllocation(id);
//        }
//        public String DeActiveUser(int LoginId)
//        { 
//            return _adminDataServiceRepository.DeActiveUser(LoginId);   
//        }
//        public List<LoginViewModel> GetAllStudent(int DeparmentId)
//        {
//            return _adminDataServiceRepository.GetAllStudent(DeparmentId);
//        }

//    }
//}
using Core.domain.Models;
using Core.Domain.Models;
using Core.Reposository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminDataServiceRepository _adminDataServiceRepository;

        public AdminService(IAdminDataServiceRepository adminDataServiceRepository)
        {
            this._adminDataServiceRepository = adminDataServiceRepository;
        }

        public async Task<List<LoginTb>> GetAllUsersAsync()
        {
            return await _adminDataServiceRepository.GetAllUsersAsync();
        }

        public async Task<List<DepartmentTb>> GetDepartmentsListAsync()
        {
            return await _adminDataServiceRepository.GetDepartmentsListAsync();
        }

        public async Task<string> RegisterUserAsync(LoginViewModel value)
        {
            return await _adminDataServiceRepository.RegisterUserAsync(value);
        }

        public async Task<LoginViewModel> LoginUserAsync(UserLoginModel value)
        {
            return await _adminDataServiceRepository.LoginUserAsync(value);
        }

        public async Task<List<LoginViewModel>> GetNewRegisterUserAsync()
        {
            return await _adminDataServiceRepository.GetNewRegisterUserAsync();
        }

        public async Task<string> ActiveUserAsync(int LoginId)
        {
            return await _adminDataServiceRepository.ActiveUserAsync(LoginId);
        }

        public async Task<SesstionTB> GetActiveSessionAsync()
        {
            return await _adminDataServiceRepository.GetActiveSessionAsync();
        }

        public async Task<List<LoginViewModel>> GetExcelOfRegisterStudentAsync(int DepartmentId)
        {
            return await _adminDataServiceRepository.GetExcelOfRegisterStudentAsync(DepartmentId);
        }

        public async Task<string> SendEmailToRegisterStudentAsync(int DepartmentId)
        {
            return await _adminDataServiceRepository.SendEmailToRegisterStudentAsync(DepartmentId);
        }

        public async Task<List<TeacherTb>> GetAllTeachersAsync()
        {
            return await _adminDataServiceRepository.GetAllTeachersAsync();
        }

        public async Task<string> SaveTeacherAsync(TeacherTb value)
        {
            return await _adminDataServiceRepository.SaveTeacherAsync(value);
        }

        public async Task<string> DeleteTeacherAsync(int TeacherId)
        {
            return await _adminDataServiceRepository.DeleteTeacherAsync(TeacherId);
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            return await _adminDataServiceRepository.GetAllCourseAsync();
        }

        public async Task<string> SaveCourseAsync(CourseViewModel value)
        {
            return await _adminDataServiceRepository.SaveCourseAsync(value);
        }

        public async Task<string> DeleteCourseAsync(int CourseId)
        {
            return await _adminDataServiceRepository.DeleteCourseAsync(CourseId);
        }

        public async Task<List<ClassRoomTb>> GetAllClassRoomAsync()
        {
            return await _adminDataServiceRepository.GetAllClassRoomAsync();
        }

        public async Task<string> SaveClassRoomAsync(ClassRoomTb value)
        {
            return await _adminDataServiceRepository.SaveClassRoomAsync(value);
        }

        public async Task<string> DeleteClassRoomAsync(int ClassRoomId)
        {
            return await _adminDataServiceRepository.DeleteClassRoomAsync(ClassRoomId);
        }

        public async Task<List<LoginTb>> GetAdminAndTeacherAsync()
        {
            return await _adminDataServiceRepository.GetAdminAndTeacherAsync();
        }

        public async Task<string> RoomAndTeacherAllocationAsync(AllocationTeacherTb value)
        {
            return await _adminDataServiceRepository.RoomAndTeacherAllocationAsync(value);
        }

        public async Task<List<AllocationTeacherViewModel>> GetRoomAndTeacherAllocationAsync()
        {
            return await _adminDataServiceRepository.GetRoomAndTeacherAllocationAsync();
        }

        public async Task<string> DeleteRoomAndTeacherAllocationAsync(int id)
        {
            return await _adminDataServiceRepository.DeleteRoomAndTeacherAllocationAsync(id);
        }

        public async Task<string> DeActiveUserAsync(int LoginId)
        {
            return await _adminDataServiceRepository.DeActiveUserAsync(LoginId);
        }

        public async Task<List<LoginViewModel>> GetAllStudentAsync(int DepartmentId)
        {
            return await _adminDataServiceRepository.GetAllStudentAsync(DepartmentId);
        }
    }
}

