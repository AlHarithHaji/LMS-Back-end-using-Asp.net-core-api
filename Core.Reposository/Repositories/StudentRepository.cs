using Core.domain.Models;
using Core.Domain.Models;
using Core.Entity.LMSDataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Reposository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LMSDbDataContext _dbContext;

        public StudentRepository(LMSDbDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get Student Course by LoginId
        public async Task<LoginViewModel> GetStudentCurrentCourseAsync(int LoginId)
        {
            var query = await (from c in _dbContext.StudentCourseAllocationTb
                               join l in _dbContext.LoginTb on c.LoginId equals l.LoginId
                               join a in _dbContext.CoursesTb on c.CourseId equals a.CourseId
                               join d in _dbContext.DepartmentTb on l.DepartmentId equals d.DepartmentId
                               where c.LoginId == LoginId && c.IsCourseActive == true
                               select new LoginViewModel
                               {
                                   FirstName = l.FirstName,
                                   LastName = l.LastName,
                                   Email = l.Email,
                                   Mobile = l.Mobile,
                                   Address = l.Address,
                                   CourseId = l.CourseId,
                                   CourseName = a.CourseName,
                                   DepartName = d.DeparmentName,
                                   DepartmentId = d.DepartmentId,

                               }).FirstOrDefaultAsync();

            return query;
        }

        //Get Student Attendance
        public async Task<List<StudentDashbordViewModel>> GetStudentAttendanceAsync(int LoginId)
        {
            var query = await (from c in _dbContext.StudentAttandenceTb
                               join a in _dbContext.CoursesTb on c.CourseId equals a.CourseId
                               where c.LoginId == LoginId
                               select new StudentDashbordViewModel
                               {
                                   CourseName = a.CourseName,
                                   TotalLectures = c.TotalLectures,
                                   Presence = c.Presents

                               }).ToListAsync();

            return query;
        }

        //Get Student Grades
        //Get Student Grades
        public async Task<List<StudentDashbordViewModel>> GetStudentGradesAsync(int LoginId)
        {
            var query = await (from c in _dbContext.StudentCourseGradesTb
                               join a in _dbContext.CoursesTb on c.CourseId equals a.CourseId
                               where c.LoginId == LoginId
                               select new StudentDashbordViewModel
                               {
                                   CourseName = a.CourseName,
                                   ObtainMarks = c.ObtainMarks,
                                   TotalMarks = c.TotalMarks,
                                   PercenTage = c.Percentage
                               }).ToListAsync();

            return query;
        }

        

        //Get Student Class And Room and Teachers
        public async Task<List<AllocationTeacherViewModel>> GetStudentRoomCourseAndTeacherAllocationAsync(int CourseId)
        {
            var query = await (from c in _dbContext.AllocationTeacherTb
                               join t in _dbContext.TeacherTb on c.TeacherId equals t.TeacherId
                               join r in _dbContext.ClassRoomTb on c.ClassRoomId equals r.ClassRoomId
                               where c.CourseId == CourseId
                               select new AllocationTeacherViewModel
                               {
                                   TeacherName = t.TeacherName,
                                   ClassRoom = r.ClassRoom,
                                   StartTime = c.StartTime,
                                   EndTime = c.EndTime,

                               }).ToListAsync();

            return query;
        }
    }
}
