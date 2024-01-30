using Core.domain.Models;
using Core.Domain.Models;
using Core.Domain.Utilities;
using Core.Domain.Utility;
using Core.Entity.LMSDataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Reposository.Repositories.AdminDataServiceRepository
{
    public class AdminDataServiceRepository : IAdminDataServiceRepository
    {
        private readonly LMSDbDataContext _dbContext;

        public AdminDataServiceRepository(LMSDbDataContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //Get: All Users
        public async Task<List<LoginTb>> GetAllUsersAsync()
        {
            return await _dbContext.LoginTb.ToListAsync();
        }
        public async Task<List<DepartmentTb>> GetDepartmentsListAsync()
        {
            return await _dbContext.DepartmentTb.ToListAsync();
        }

        //Get: Teacher and Admin Users
        public async Task<List<LoginTb>> GetAdminAndTeacherAsync()
        {
            return await _dbContext.LoginTb
                .Where(x => x.UserType == "Teacher" || x.UserType == "Admin")
                .ToListAsync();
        }

        //Get All Departments
        public async Task<List<DepartmentTb>> GetDeparmentsListAsync()
        {
            return await _dbContext.DepartmentTb.ToListAsync();
        }

        //Get Active Session
        public async Task<SesstionTB> GetActiveSessionAsync()
        {
            return await _dbContext.SesstionTB
                .Where(x => x.IsActive == true)
                .FirstOrDefaultAsync();
        }

        //Post Register user
        public async Task<string> RegisterUserAsync(LoginViewModel value)
        {
            var CheckForUpdate = await _dbContext.LoginTb
                .Where(x => x.LoginId == value.LoginId)
                .FirstOrDefaultAsync();

            if (CheckForUpdate == null)
            {
                var CheckLoginbefore = await _dbContext.LoginTb
                    .Where(x => x.Email == value.Email.Trim())
                    .FirstOrDefaultAsync();

                var RegisterCount = await GetRegisterStudentCountAsync((int)value.SessionId);

                if (RegisterCount < 20)
                {
                    if (CheckLoginbefore == null)
                    {
                        LoginTb loginTb = new LoginTb
                        {
                            Address = value.Address,
                            Password = EncriptionMethod.EncryptString(value.Password),
                            IsActive = false,  // Keep this only once
                            Email = value.Email,
                            FirstName = value.FirstName,
                            LastName = value.LastName,
                            Mobile = value.Mobile,
                            UserType = value.UserType,
                            DepartmentId = value.DepartmentId,
                            CourseId = await GetCourseIdByDeparmentAsync(value.DepartmentId),
                            SessionId = (await GetActiveSessionAsync()).SesstionId
                        };

                        _dbContext.LoginTb.Add(loginTb);
                        await _dbContext.SaveChangesAsync();

                        if (value.DepartmentId == 2)
                        {
                            var Message = "Dear " + value.FirstName + value.LastName +
                                "\r\n\r\nWe are thrilled to inform you that your application for admission has been Received." +
                                "The Mobile Development is Payable, So you have to do payment before admission." +
                                "Account-N0 (IBAN) ABC0099948484848, Fee: 300 USD, after that payment you have to email AdminLMS@gmail.com journey ahead." +
                                "\r\n\r\nBest wishes";

                            EmailService.SendEmail(value.Email, Message, "LMS-Admission");

                            return "Your request for Mobile Payment has been received, we send payment procedure on your email.";
                        }

                        return "Save Successfully";
                    }
                    else
                    {
                        return "Email already Register";
                    }
                }
                else
                {
                    return "Student Register count complete; please select another session";
                }
            }
            else
            {
                CheckForUpdate.Address = value.Address;
                CheckForUpdate.FirstName = value.FirstName;
                CheckForUpdate.LastName = value.LastName;
                CheckForUpdate.Mobile = value.Mobile;
                CheckForUpdate.UserType = value.UserType;

                await _dbContext.SaveChangesAsync();

                return "Update Successfully";
            }
        }

        private async Task<int?> GetCourseIdByDeparmentAsync(int departmentId)
        {
            return await _dbContext.CoursesTb
                .Where(x => x.DeparmentId == departmentId && x.IsDeleted == false)
                .OrderBy(x => x.CourseId)
                .Select(x => x.CourseId)
                .FirstOrDefaultAsync();
        }

        //Post Register user
        public async Task<LoginViewModel> LoginUserAsync(UserLoginModel value)
        {
            var encryptedPassword = EncriptionMethod.EncryptString(value.Password);

            var query = await (from c in _dbContext.LoginTb
                               join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId into departmentGroup
                               from a in departmentGroup.DefaultIfEmpty()
                               where c.Email == value.Email && c.Password == encryptedPassword
                               select new LoginViewModel
                               {
                                   FirstName = c.FirstName,
                                   LastName = c.LastName,
                                   Email = c.Email,
                                   DepartmentId = c.DepartmentId,
                                   DepartName = a.DeparmentName,
                                   IsActive = c.IsActive,
                                   UserType = c.UserType,
                                   Address = c.Address,
                                   LoginId = c.LoginId
                               }).FirstOrDefaultAsync();

            return query;
        }

        //Get: All new Register users
        public async Task<List<LoginViewModel>> GetNewRegisterUserAsync()
        {
            var query = await (from c in _dbContext.LoginTb
                               join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId
                               where c.IsActive == false
                               select new LoginViewModel
                               {
                                   FirstName = c.FirstName,
                                   LastName = c.LastName,
                                   Email = c.Email,
                                   DepartmentId = c.DepartmentId,
                                   DepartName = a.DeparmentName,
                                   IsActive = c.IsActive,
                                   UserType = c.UserType,
                                   Address = c.Address,
                                   LoginId = c.LoginId
                               }).ToListAsync();

            return query;
        }

        //Active User
        public async Task<string> ActiveUserAsync(int LoginId)
        {
            var query = await _dbContext.LoginTb
                .Where(c => c.LoginId == LoginId)
                .FirstOrDefaultAsync();

            if (query != null)
            {
                query.IsActive = true;

                await _dbContext.SaveChangesAsync();

                return "Register to Active user";
            }
            else
            {
                return "Failed to Active user";
            }
        }

        public async Task<string> DeActiveUserAsync(int LoginId)
        {
            var query = await _dbContext.LoginTb
                .Where(c => c.LoginId == LoginId)
                .FirstOrDefaultAsync();

            if (query != null)
            {
                query.IsActive = null;

                await _dbContext.SaveChangesAsync();

                return "Register to Active user";
            }
            else
            {
                return "Failed to Active user";
            }
        }

        //Get Student Count
        public async Task<int> GetRegisterStudentCountAsync(int SessionId)
        {
            return await _dbContext.SesstionTB
                .Where(x => x.SesstionId == SessionId)
                .CountAsync();
        }

        //Get Generate Excel for Register students
        public async Task<List<LoginViewModel>> GetExcelOfRegisterStudentAsync(int DeparmentId)
        {
            int activeSession = (await GetActiveSessionAsync()).SesstionId;

            var query = await (from c in _dbContext.LoginTb
                               join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId
                               where c.SessionId == activeSession && c.DepartmentId == DeparmentId && c.IsActive == true
                               select new LoginViewModel
                               {
                                   FirstName = c.FirstName,
                                   LastName = c.LastName,
                                   Email = c.Email,
                                   DepartmentId = c.DepartmentId,
                                   DepartName = a.DeparmentName,
                                   IsActive = c.IsActive ?? false,
                                   UserType = c.UserType,
                                   Address = c.Address,
                                   LoginId = c.LoginId
                               }).ToListAsync();

            List<LoginViewModel> newAdmission = new List<LoginViewModel>();

            foreach (var q in query)
            {
                var checkAlreadyPresence = await _dbContext.StudentCourseAllocationTb
                    .Where(x => x.LoginId == q.LoginId)
                    .AnyAsync();

                if (!checkAlreadyPresence)
                {
                    newAdmission.Add(q);
                }
            }

            return newAdmission;
        }

        public async Task<List<LoginViewModel>> GetAllStudentAsync(int DeparmentId)
        {
            var query = await (from c in _dbContext.LoginTb
                               join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId
                               where c.DepartmentId == DeparmentId && c.IsActive == true
                               select new LoginViewModel
                               {
                                   FirstName = c.FirstName,
                                   LastName = c.LastName,
                                   Email = c.Email,
                                   DepartmentId = c.DepartmentId,
                                   DepartName = a.DeparmentName,
                                   IsActive = c.IsActive ?? false,
                                   UserType = c.UserType,
                                   Address = c.Address,
                                   LoginId = c.LoginId
                               }).ToListAsync();

            return query;
        }

        //Send email to Students that has registered
        public async Task<string> SendEmailToRegisterStudentAsync(int DeparmentId)
        {
            int activeSession = (await GetActiveSessionAsync()).SesstionId;

            var query = await (from c in _dbContext.LoginTb
                               join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId
                               where c.SessionId == activeSession && c.DepartmentId == DeparmentId && c.IsActive == true
                               select new LoginViewModel
                               {
                                   FirstName = c.FirstName,
                                   LastName = c.LastName,
                                   Email = c.Email,
                                   DepartmentId = c.DepartmentId,
                                   DepartName = a.DeparmentName,
                                   IsActive = c.IsActive,
                                   UserType = c.UserType,
                                   Address = c.Address,
                                   LoginId = c.LoginId,
                                   CourseId = c.CourseId,
                                   SessionId = c.SessionId
                               }).ToListAsync();

            foreach (var d in query)
            {
                await SetStudentCourseAllocationAsync(d);

                var message = "Dear " + d.FirstName + d.LastName +
                    "\r\n\r\nWe are thrilled to inform you that your application for admission has been approved! Congratulations on this significant achievement, and welcome to our esteemed \r\n\r\nYour dedication and hard work have truly set you apart, and we believe that your presence will greatly contribute to the vibrant academic community we cherish. We are confident that your unique perspective and talents will enhance the overall experience for everyone.\r\n\r\nYour journey with us promises exciting opportunities for growth, learning, and success. We look forward to witnessing your accomplishments and the positive impact we know you will make.\r\n\r\nPlease take this moment to celebrate your well-deserved success. As you embark on this new chapter, we wish you the very best of luck! We can't wait to see you at [Orientation/Start Date], where you will meet fellow students, faculty members, and start creating lasting memories.\r\n\r\nIf you have any questions or need assistance with the next steps, feel free to reach out to [Contact Person/Department] at [Contact Email/Phone]. We are here to support you every step of the way.\r\n\r\nOnce again, congratulations on your admission, and we eagerly anticipate the remarkable journey ahead.\r\n\r\nBest wishes";

                EmailService.SendEmail(d.Email, message, "LMS-Admission");
            }

            return "Email has been sent";
        }

        public async Task<string> SetStudentCourseAllocationAsync(LoginViewModel value)
        {
            StudentCourseAllocationTb studentCourseAllocationTb = new StudentCourseAllocationTb
            {
                IsCourseActive = true,
                CourseId = (int)value.CourseId,
                LoginId = (int)value.LoginId,
                SesstionId = (int)value.SessionId
            };

            _dbContext.StudentCourseAllocationTb.Add(studentCourseAllocationTb);
            await _dbContext.SaveChangesAsync();

            return "";
        }

        #region TeacherManage
        //Get All Teachers
        public async Task<List<TeacherTb>> GetAllTeachersAsync()
        {
            return await _dbContext.TeacherTb.ToListAsync();
        }

        //Get Save Update Teachers
        public async Task<string> SaveTeacherAsync(TeacherTb value)
        {
            var updateQuery = await _dbContext.TeacherTb
                .Where(x => x.TeacherId == value.TeacherId)
                .FirstOrDefaultAsync();

            if (updateQuery != null)
            {
                updateQuery.TeacherName = value.TeacherName;
                await _dbContext.SaveChangesAsync();
                return "Update Successfully";
            }
            else
            {
                TeacherTb teacherTb = new TeacherTb
                {
                    TeacherName = value.TeacherName,
                    IsDeleted = false
                };

                _dbContext.TeacherTb.Add(teacherTb);
                await _dbContext.SaveChangesAsync();
                return "Save Successfully";
            }
        }

        //Delete: Delete Teacher 
        public async Task<string> DeleteTeacherAsync(int TeacherId)
        {
            var updateQuery = await _dbContext.TeacherTb
                .Where(x => x.TeacherId == TeacherId)
                .FirstOrDefaultAsync();

            if (updateQuery != null)
            {
                updateQuery.IsDeleted = false;
                await _dbContext.SaveChangesAsync();
                return "Update Successfully";
            }
            else
            {
                return "Failed to Delete";
            }
        }

        #endregion

        #region ManageCourse
        //Get All Teacher
        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            return await (from c in _dbContext.CoursesTb
                          join d in _dbContext.DepartmentTb on c.DeparmentId equals d.DepartmentId
                          select new CourseViewModel
                          {
                              CourseName = c.CourseName,
                              DeparmentId = c.DeparmentId,
                              CourseId = c.CourseId,
                              DeparmentName = d.DeparmentName,
                              IsDeleted = c.IsDeleted
                          }).ToListAsync();
        }

        //Get Save Update Teachers
        public async Task<string> SaveCourseAsync(CourseViewModel value)
        {
            var updateQuery = await _dbContext.CoursesTb
                .Where(x => x.CourseId == value.CourseId)
                .FirstOrDefaultAsync();

            if (updateQuery != null)
            {
                updateQuery.CourseName = value.CourseName;
                updateQuery.DeparmentId = value.DeparmentId;
                await _dbContext.SaveChangesAsync();
                return "Update Successfully";
            }
            else
            {
                CoursesTb teacherTb = new CoursesTb
                {
                    CourseName = value.CourseName,
                    DeparmentId = value.DeparmentId,
                    IsDeleted = false
                };

                _dbContext.CoursesTb.Add(teacherTb);
                await _dbContext.SaveChangesAsync();
                return "Save Successfully";
            }
        }

        //Delete: Delete Teacher 
        public async Task<string> DeleteCourseAsync(int CourseId)
        {
            var updateQuery = await _dbContext.CoursesTb
                .Where(x => x.CourseId == CourseId)
                .FirstOrDefaultAsync();

            if (updateQuery != null)
            {
                updateQuery.IsDeleted = false;
                await _dbContext.SaveChangesAsync();
                return "Update Successfully";
            }
            else
            {
                return "Failed to Delete";
            }
        }
        #endregion

        #region ClassRoom
        //Get All Class Room
        public async Task<List<ClassRoomTb>> GetAllClassRoomAsync()
        {
            return await _dbContext.ClassRoomTb
                .Where(x => x.IsDeleted == false)
                .ToListAsync();
        }

        //Save Class room
        public async Task<string> SaveClassRoomAsync(ClassRoomTb value)
        {
            var updateQuery = await _dbContext.ClassRoomTb
                .Where(x => x.ClassRoomId == value.ClassRoomId)
                .FirstOrDefaultAsync();

            if (updateQuery != null)
            {
                updateQuery.ClassRoom = value.ClassRoom;
                await _dbContext.SaveChangesAsync();
                return "Update Successfully";
            }
            else
            {
                ClassRoomTb classRoomTb = new ClassRoomTb
                {
                    ClassRoom = value.ClassRoom,
                    IsDeleted = false
                };

                _dbContext.ClassRoomTb.Add(classRoomTb);
                await _dbContext.SaveChangesAsync();
                return "Save Successfully";
            }
        }

        public async Task<string> DeleteClassRoomAsync(int ClassRoomId)
        {
            var updateQuery = await _dbContext.ClassRoomTb
                .Where(x => x.ClassRoomId == ClassRoomId)
                .FirstOrDefaultAsync();

            if (updateQuery != null)
            {
                updateQuery.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
                return "Update Successfully";
            }
            else
            {
                return "Failed to Delete";
            }
        }
        #endregion

        #region RoomAndTeacherAllocation
        public async Task<string> RoomAndTeacherAllocationAsync(AllocationTeacherTb value)
        {
            bool isTeacherAvailable = await CheckTeachAvailableAsync(value);
            bool isClassRoomAvailable = await CheckClassRoomAvailableAsync(value);

            if (isTeacherAvailable)
            {
                return "Teacher is already Busy in this duration";
            }

            if (isClassRoomAvailable)
            {
                return "Classroom is already allocated in this duration";
            }

            AllocationTeacherTb allocationTeacherTb = new AllocationTeacherTb
            {
                TeacherId = value.TeacherId,
                CourseId = value.CourseId,
                ClassRoomId = value.ClassRoomId,
                StartTime = value.StartTime,
                EndTime = value.EndTime
            };

            _dbContext.AllocationTeacherTb.Add(allocationTeacherTb);
            await _dbContext.SaveChangesAsync();

            return "Save Successfully";
        }

        public async Task<List<AllocationTeacherViewModel>> GetRoomAndTeacherAllocationAsync()
        {
            var query = await (from c in _dbContext.AllocationTeacherTb
                               join d in _dbContext.CoursesTb on c.CourseId equals d.CourseId
                               join a in _dbContext.ClassRoomTb on c.ClassRoomId equals a.ClassRoomId
                               join t in _dbContext.TeacherTb on c.TeacherId equals t.TeacherId
                               select new AllocationTeacherViewModel
                               {
                                   ClassRoom = a.ClassRoom,
                                   TeacherName = t.TeacherName,
                                   CourseName = d.CourseName,
                                   ClassRoomId = c.ClassRoomId,
                                   CourseId = c.CourseId,
                                   AllocationTeacherId = c.AllocationTeacherId,
                                   StartTime = c.StartTime,
                                   EndTime = c.EndTime
                               }).ToListAsync();

            return query;
        }

        public async Task<string> DeleteRoomAndTeacherAllocationAsync(int id)
        {
            var query = await _dbContext.AllocationTeacherTb
                .Where(x => x.AllocationTeacherId == id)
                .FirstOrDefaultAsync();

            if (query != null)
            {
                _dbContext.AllocationTeacherTb.Remove(query);
                await _dbContext.SaveChangesAsync();
                return "Deleted Successfully";
            }
            else
            {
                return "Error while delete";
            }
        }

        public async Task<bool> CheckClassRoomAvailableAsync(AllocationTeacherTb value)
        {
            return await _dbContext.AllocationTeacherTb
                .Where(x => x.ClassRoomId == value.ClassRoomId &&
                            x.StartTime.Value <= value.EndTime &&
                            x.EndTime.Value >= value.StartTime)
                .AnyAsync();
        }

        public async Task<bool> CheckTeachAvailableAsync(AllocationTeacherTb value)
        {
            return await _dbContext.AllocationTeacherTb
                .Where(x => x.TeacherId == value.TeacherId &&
                            x.StartTime.Value <= value.EndTime &&
                            x.EndTime.Value >= value.StartTime)
                .AnyAsync();
        }
        #endregion
    }
}





//Get: All Users
//public List<LoginTb> GetAllUsers()
//{
//    return _dbContext.LoginTb.ToList();
//}
////Get: Teacher and Admin Users
//public List<LoginTb> GetAdminAndTeacher()
//{
//    return _dbContext.LoginTb.Where(x=>x.UserType=="Teacher" ||x.UserType=="Admin"  ).ToList();
//}
////Get All Deparments
//public List<DepartmentTb> GetDeparmentsList() {
//    return _dbContext.DepartmentTb.ToList();
//}
////Get Active Session
//public SesstionTB GetActiveSession()
//{
//    return _dbContext.SesstionTB.Where(x=>x.IsActive==true).FirstOrDefault();
//}
////Post Register user 
//public String RegisterUser(LoginViewModel value)
//{
//    var CheckForUpdate= _dbContext.LoginTb.Where(x => x.LoginId == value.LoginId).FirstOrDefault();
//    if (CheckForUpdate == null)
//    {
//        var CheckLoginbefore = _dbContext.LoginTb.Where(x => x.Email == value.Email.Trim()).FirstOrDefault();
//        var RegisterCount = GetRegisterStudentCount((int)value.SessionId);
//        if (RegisterCount < 20)
//        {
//            if (CheckLoginbefore == null)
//            {

//                LoginTb loginTb = new LoginTb();
//                loginTb.Address = value.Address;
//                loginTb.Password = EncriptionMethod.EncryptString(value.Password);
//                loginTb.IsActive = false;
//                loginTb.Email = value.Email;
//                loginTb.FirstName = value.FirstName;
//                loginTb.LastName = value.LastName;
//                loginTb.Mobile = value.Mobile;
//                loginTb.UserType = value.UserType;
//                loginTb.DepartmentId = value.DepartmentId;
//                loginTb.IsActive = false;
//                loginTb.CourseId = GetCourseIdByDeparment(value.DepartmentId);
//                loginTb.SessionId = GetActiveSession().SesstionId;
//                _dbContext.LoginTb.Add(loginTb);
//                _dbContext.SaveChanges();
//                if (value.DepartmentId == 2)
//                {
//                    var Message = "Dear " + value.FirstName + value.LastName + "\r\n\r\nWe are thrilled to inform you that your application for admission has been Received.The Mobile Develoment is Payable, So you have do payment before admission. Account-N0 (IBAN) ABC0099948484848, Fee: 300 USD, after that payment you have email AdminLMS@gmail.com  journey ahead.\r\n\r\nBest wishes";
//                    EmailService.SendEmail(value.Email, Message, "LMS-Admission");
//                    return "Your request for Mobile Payment has been receivced, we send payment procedure on you email.";
//                }
//                return "Save Successfully";
//            }
//            else
//            {
//                return "Email already Register";
//            }
//        }
//        else
//        {
//            return "Student Register count complete you make select other session";
//        }
//    }
//    else {
//        CheckForUpdate.Address = value.Address;
//        CheckForUpdate.FirstName = value.FirstName;
//        CheckForUpdate.LastName = value.LastName;
//        CheckForUpdate.Mobile = value.Mobile;
//        CheckForUpdate.UserType = value.UserType;
//        _dbContext.SaveChanges();
//        return "Update Successfully";
//    }


//}

//private int? GetCourseIdByDeparment(int departmentId)
//{
//    return _dbContext.CoursesTb.Where(x => x.DeparmentId == departmentId && x.IsDeleted == false).OrderBy(x => x.CourseId).Select(x => x.CourseId).FirstOrDefault();
//}

////Post Register user 
//public LoginViewModel LoginUser(UserLoginModel value)
//{
//    var EncriptPassword = EncriptionMethod.EncryptString(value.Password);

//    var Query = (from c in _dbContext.LoginTb
//                 join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId into departmentGroup
//                 from a in departmentGroup.DefaultIfEmpty()
//                 where c.Email == value.Email && c.Password == EncriptPassword
//                 select new LoginViewModel
//                 {
//                     FirstName = c.FirstName,
//                     LastName = c.LastName,
//                     Email = c.Email,
//                     DepartmentId = c.DepartmentId,
//                     DepartName = a.DeparmentName,
//                     IsActive = c.IsActive,
//                     UserType = c.UserType,
//                     Address = c.Address,
//                     LoginId = c.LoginId
//                 }).FirstOrDefault();

//    return Query;


//}
////Get: All new Register users
//public List<LoginViewModel> GetNewRegisterUser()
//{
//    var Query = (from c in _dbContext.LoginTb
//                 join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId
//                 where c.IsActive == false
//                 select new LoginViewModel
//                 {
//                     FirstName = c.FirstName,
//                     LastName = c.LastName,
//                     Email = c.Email,
//                     DepartmentId = c.DepartmentId,
//                     DepartName = a.DeparmentName,
//                     IsActive = c.IsActive,
//                     UserType = c.UserType,
//                     Address = c.Address,
//                     LoginId = c.LoginId
//                 }).ToList();
//    return Query;
//}
////Active User 
//public String ActiveUser(int LoginId)
//{
//    var Query = (from c in _dbContext.LoginTb
//                 where c.LoginId == LoginId
//                 select c).FirstOrDefault();
//    if (Query != null)
//    {
//        Query.IsActive = true;
//        _dbContext.SaveChanges();
//        return "Register to Active user";
//    }
//    else {
//        return "Falied to Active user";
//    }


//}
//public String DeActiveUser(int LoginId)
//{
//    var Query = (from c in _dbContext.LoginTb
//                 where c.LoginId == LoginId
//                 select c).FirstOrDefault();
//    if (Query != null)
//    {
//        Query.IsActive = null;
//        _dbContext.SaveChanges();
//        return "Register to Active user";
//    }
//    else
//    {
//        return "Falied to Active user";
//    }


//}
////Get Student Count
//public int GetRegisterStudentCount(int SessionId)
//{
//    return _dbContext.SesstionTB.Where(x => x.SesstionId == SessionId).Count();
//}
////Get Generate Excel for Register students
//public List<LoginViewModel> GetExcelOfRegisterStudent(int DeparmentId)
//{
//    int ActiveSession = GetActiveSession().SesstionId;
//    var Query = (from c in _dbContext.LoginTb
//                 join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId
//                 where c.SessionId == ActiveSession && c.DepartmentId == DeparmentId && c.IsActive==true
//                 select new LoginViewModel
//                 {
//                     FirstName = c.FirstName,
//                     LastName = c.LastName,
//                     Email = c.Email,
//                     DepartmentId = c.DepartmentId,
//                     DepartName = a.DeparmentName,
//                     IsActive = c.IsActive ?? false,
//                     UserType = c.UserType,
//                     Address = c.Address,
//                     LoginId = c.LoginId
//                 }).ToList();
//    List<LoginViewModel> NewAdmission = new List<LoginViewModel>();
//    foreach (var q in Query)
//    {
//        var CheckAlreadyPresence = _dbContext.StudentCourseAllocationTb.Where(x => x.LoginId == q.LoginId).Any();
//        if (CheckAlreadyPresence == false)
//        {
//            NewAdmission.Add(q);
//        }

//    }

//    return NewAdmission;
//}
//public List<LoginViewModel> GetAllStudent(int DeparmentId)
//{

//    var Query = (from c in _dbContext.LoginTb
//                 join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId
//                 where  c.DepartmentId == DeparmentId && c.IsActive == true
//                 select new LoginViewModel
//                 {
//                     FirstName = c.FirstName,
//                     LastName = c.LastName,
//                     Email = c.Email,
//                     DepartmentId = c.DepartmentId,
//                     DepartName = a.DeparmentName,
//                     IsActive = c.IsActive ?? false,
//                     UserType = c.UserType,
//                     Address = c.Address,
//                     LoginId = c.LoginId
//                 }).ToList();

//    return Query;
//}
////Send email to Students that has register
//public String SendEmailToRegisterStudent(int DeparmentId)
//{
//    int ActiveSession = GetActiveSession().SesstionId;
//    var Query = (from c in _dbContext.LoginTb
//                 join a in _dbContext.DepartmentTb on c.DepartmentId equals a.DepartmentId
//                 where c.SessionId == ActiveSession && c.DepartmentId == DeparmentId && c.IsActive==true
//                 select new LoginViewModel
//                 {
//                     FirstName = c.FirstName,
//                     LastName = c.LastName,
//                     Email = c.Email,
//                     DepartmentId = c.DepartmentId,
//                     DepartName = a.DeparmentName,
//                     IsActive = c.IsActive,
//                     UserType = c.UserType,
//                     Address = c.Address,
//                     LoginId = c.LoginId,
//                     CourseId=c.CourseId,
//                     SessionId=c.SessionId

//                 }).ToList();
//    foreach (var d in Query)
//    {
//        SetStudentCourseAllocation(d);
//        var Message = "Dear " + d.FirstName + d.LastName + "\r\n\r\nWe are thrilled to inform you that your application for admission has been approved! Congratulations on this significant achievement, and welcome to our esteemed \r\n\r\nYour dedication and hard work have truly set you apart, and we believe that your presence will greatly contribute to the vibrant academic community we cherish. We are confident that your unique perspective and talents will enhance the overall experience for everyone.\r\n\r\nYour journey with us promises exciting opportunities for growth, learning, and success. We look forward to witnessing your accomplishments and the positive impact we know you will make.\r\n\r\nPlease take this moment to celebrate your well-deserved success. As you embark on this new chapter, we wish you the very best of luck! We can't wait to see you at [Orientation/Start Date], where you will meet fellow students, faculty members, and start creating lasting memories.\r\n\r\nIf you have any questions or need assistance with the next steps, feel free to reach out to [Contact Person/Department] at [Contact Email/Phone]. We are here to support you every step of the way.\r\n\r\nOnce again, congratulations on your admission, and we eagerly anticipate the remarkable journey ahead.\r\n\r\nBest wishes";
//        EmailService.SendEmail(d.Email, Message, "LMS-Admission");
//        //System.Threading.Thread.Sleep(10000);
//    }
//    return "Email has be send";
//}
//public String SetStudentCourseAllocation(LoginViewModel value)
//{ 
//    StudentCourseAllocationTb studentCourseAllocationTb = new StudentCourseAllocationTb();
//    studentCourseAllocationTb.IsCourseActive = true;
//    studentCourseAllocationTb.CourseId = (int)value.CourseId;
//    studentCourseAllocationTb.LoginId = (int)value.LoginId;
//    studentCourseAllocationTb.SesstionId =(int) value.SessionId;
//    _dbContext.StudentCourseAllocationTb.Add(studentCourseAllocationTb);
//    _dbContext.SaveChanges();
//    return "";

//}
//    #region TeacherManage
//    //Get//All Teachers
//    public List<TeacherTb> GetAllTeachers()
//{
//    return _dbContext.TeacherTb.ToList();
//}
////Get Save Update Teachers
//public String SaveTeacher(TeacherTb value)
//{
//    var UpdateQuery = _dbContext.TeacherTb.Where(x => x.TeacherId == value.TeacherId).FirstOrDefault();
//    if (UpdateQuery != null)
//    {
//        UpdateQuery.TeacherName = value.TeacherName;
//        _dbContext.SaveChanges();
//        return "Update Successfully";
//    }
//    else
//    { 
//        TeacherTb teacherTb=new TeacherTb();
//        teacherTb.TeacherName=value.TeacherName;
//        teacherTb.IsDeleted = false;
//        _dbContext.TeacherTb.Add(teacherTb);
//        _dbContext.SaveChanges();
//        return "Save Successfully";
//    }
//}
////Delete: Delete Teacher 
//public string DeleteTeacher(int TeacherId)
//{
//    var UpdateQuery = _dbContext.TeacherTb.Where(x => x.TeacherId == TeacherId).FirstOrDefault();
//    if (UpdateQuery != null)
//    {
//        UpdateQuery.IsDeleted = false;
//        _dbContext.SaveChanges();
//        return "Update Successfully";
//    }
//    else
//    {
//        return "Failed to Delete";
//    }
//}

//#endregion

//#region ManageCourse
////Get All Teacher
//public List<CourseViewModel> GetAllCourse()
//{
//    return (from c in _dbContext.CoursesTb 
//            join d in _dbContext.DepartmentTb on c.DeparmentId equals d.DepartmentId
//            select new CourseViewModel {
//             CourseName=c.CourseName,
//              DeparmentId=c.DeparmentId,
//               CourseId=c.CourseId,
//                DeparmentName=d.DeparmentName,
//                 IsDeleted=c.IsDeleted
//            }).ToList();
//}
////Get Save Update Teachers
//public String SaveCourse(CourseViewModel value)
//{
//    var UpdateQuery = _dbContext.CoursesTb.Where(x => x.CourseId == value.CourseId).FirstOrDefault();
//    if (UpdateQuery != null)
//    {
//        UpdateQuery.CourseName = value.CourseName;
//        UpdateQuery.DeparmentId = value.DeparmentId;
//        _dbContext.SaveChanges();
//        return "Update Successfully";
//    }
//    else
//    {
//        CoursesTb teacherTb = new CoursesTb();
//        teacherTb.CourseName = value.CourseName;
//        teacherTb.DeparmentId = value.DeparmentId;
//        teacherTb.IsDeleted = false;
//        _dbContext.CoursesTb.Add(teacherTb);
//        _dbContext.SaveChanges();
//        return "Save Successfully";
//    }
//}
////Delete: Delete Teacher 
//public string DeleteCourse(int CourseId)
//{
//    var UpdateQuery = _dbContext.CoursesTb.Where(x => x.CourseId == CourseId).FirstOrDefault();
//    if (UpdateQuery != null)
//    {
//        UpdateQuery.IsDeleted = false;
//        _dbContext.SaveChanges();
//        return "Update Successfully";
//    }
//    else
//    {
//        return "Failed to Delete";
//    }
//}
//#endregion
//#region ClassRoom
////Get All Class Room
//public List<ClassRoomTb> GetAllClassRoom()
//{
//    return _dbContext.ClassRoomTb.Where(x=>x.IsDeleted==false).ToList();
//}
////Save Class room
//public String SaveClassRoom(ClassRoomTb value)
//{
//    var UpdateQuery = _dbContext.ClassRoomTb.Where(x => x.ClassRoomId == value.ClassRoomId).FirstOrDefault();
//    if (UpdateQuery != null)
//    {
//        UpdateQuery.ClassRoom = value.ClassRoom;              
//        _dbContext.SaveChanges();
//        return "Update Successfully";
//    }
//    else
//    {
//        ClassRoomTb classRoomTb = new ClassRoomTb();
//        classRoomTb.ClassRoom = value.ClassRoom;
//        classRoomTb.IsDeleted = false;
//        _dbContext.ClassRoomTb.Add(classRoomTb);
//        _dbContext.SaveChanges();
//        return "Save Successfully";
//    }
//}
//public string DeleteClassRoom(int ClassRoomId)
//{
//    var UpdateQuery = _dbContext.ClassRoomTb.Where(x => x.ClassRoomId == ClassRoomId).FirstOrDefault();
//    if (UpdateQuery != null)
//    {
//        UpdateQuery.IsDeleted = true;
//        _dbContext.SaveChanges();
//        return "Update Successfully";
//    }
//    else
//    {
//        return "Failed to Delete";
//    }
//}
//#endregion

//#region RoomAndTeacherAllocation
//public String RoomAndTeacherAllocation(AllocationTeacherTb value)
//{
//    bool isTeacherAvailable = CheckTeachAvailable(value);
//    bool IsClassRoomAvailable = CheckClassRoomAvailable(value);
//    if (isTeacherAvailable==true)
//    {
//        return "Teacher is already Busy in this duration";
//    }
//    if (IsClassRoomAvailable == true)
//    {
//        return "Classroom is already allocated in this duration";
//    }

//    AllocationTeacherTb allocationTeacherTb = new AllocationTeacherTb();    
//    allocationTeacherTb.TeacherId  = value.TeacherId;
//    allocationTeacherTb.CourseId = value.CourseId;
//    allocationTeacherTb.ClassRoomId = value.ClassRoomId;
//    allocationTeacherTb.StartTime = value.StartTime;
//    allocationTeacherTb.EndTime = value.EndTime;
//    _dbContext.AllocationTeacherTb.Add(allocationTeacherTb);
//    _dbContext.SaveChanges();
//    return "Save Sucessfully";


//}
//public List<AllocationTeacherViewModel> GetRoomAndTeacherAllocation()
//{
//    var Query = (from c in _dbContext.AllocationTeacherTb
//                 join d in _dbContext.CoursesTb on c.CourseId equals d.CourseId
//                 join a in _dbContext.ClassRoomTb on c.ClassRoomId equals a.ClassRoomId
//                 join t in _dbContext.TeacherTb on c.TeacherId equals t.TeacherId
//                 select new AllocationTeacherViewModel { 
//                 ClassRoom=a.ClassRoom,
//                 TeacherName=t.TeacherName,
//                 CourseName=d.CourseName,
//                 ClassRoomId=c.ClassRoomId,
//                 CourseId=c.CourseId,
//                 AllocationTeacherId=c.AllocationTeacherId,
//                 StartTime=c.StartTime,
//                 EndTime=c.EndTime

//                 }).ToList();
//    return Query;
//}
//public String DeleteRoomAndTeacherAllocation(int id)
//{ 
//    var Query=_dbContext.AllocationTeacherTb.Where(x=>x.AllocationTeacherId==id).FirstOrDefault();
//    if (Query != null)
//    {
//        _dbContext.AllocationTeacherTb.Remove(Query);
//        _dbContext.SaveChanges();
//        return "Deleted Successfully";
//    }
//    else {
//        return "Error while delete";
//    }
//}
//private bool CheckClassRoomAvailable(AllocationTeacherTb value)
//{
//    return _dbContext.AllocationTeacherTb
//                 .Where(x => x.ClassRoomId == value.ClassRoomId &&
//                             x.StartTime.Value <= value.EndTime &&
//                             x.EndTime.Value >= value.StartTime)
//                 .Any();

//}

//private bool CheckTeachAvailable(AllocationTeacherTb value)
//{
//    return _dbContext.AllocationTeacherTb
//                  .Where(x => x.TeacherId == value.TeacherId &&
//                              x.StartTime.Value <= value.EndTime &&
//                              x.EndTime.Value >= value.StartTime)
//                  .Any();
//}
//#endregion
