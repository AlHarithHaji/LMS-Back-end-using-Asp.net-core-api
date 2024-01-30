using Core.domain.Models;
using Core.Domain.Models;
using Core.Entity.LMSDataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Reposository.Repositories
{
    public class TeacherRespository : ITeacherRespository
    {
        private readonly LMSDbDataContext _dbContext;

        public TeacherRespository(LMSDbDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StudentCourseAllocationViewModel>> GetStudentsAttendanceAsync(int CourseId, int DeparmentId)
        {
            var CurrentSessionId = await _dbContext.SesstionTB
                .Where(x => x.IsActive == true)
                .Select(x => x.SesstionId)
                .FirstOrDefaultAsync();

            var CheckAlreadyAdded = await _dbContext.StudentAttandenceTb
                .Where(x => x.SessionId == CurrentSessionId && x.CourseId == CourseId && x.DeparmentId == DeparmentId)
                .AnyAsync();

            if (!CheckAlreadyAdded)
            {
                var query = await (from c in _dbContext.StudentCourseAllocationTb
                                   join s in _dbContext.LoginTb on c.LoginId equals s.LoginId
                                   join a in _dbContext.CoursesTb on c.CourseId equals a.CourseId
                                   join f in _dbContext.SesstionTB on c.SesstionId equals f.SesstionId
                                   where c.CourseId == CourseId && s.DepartmentId == DeparmentId && c.SesstionId == CurrentSessionId
                                   select new StudentCourseAllocationViewModel
                                   {
                                       CourseName = a.CourseName,
                                       Email = s.Email,
                                       StudentName = s.FirstName + " " + s.LastName,
                                       CourseId = CourseId,
                                       IsCourseActive = c.IsCourseActive,
                                       LoginId = s.LoginId,
                                       SesstionId = c.SesstionId,
                                       SesstionName = f.Session,
                                       DepartmentId = s.DepartmentId,
                                       StudentCourseAllocationId = c.StudentCourseAllocationId
                                   }).ToListAsync();

                return query;
            }
            else
            {
                return new List<StudentCourseAllocationViewModel>();
            }
        }

        public async Task<List<StudentCourseAllocationViewModel>> GetStudentsGradeAsync(int CourseId, int DeparmentId)
        {
            var CurrentSessionId = await _dbContext.SesstionTB
                .Where(x => x.IsActive == true)
                .Select(x => x.SesstionId)
                .FirstOrDefaultAsync();

            var CheckAlreadyAdded = await _dbContext.StudentCourseGradesTb
                .Where(x => x.SessionId == CurrentSessionId && x.CourseId == CourseId && x.DeparmentId == DeparmentId)
                .AnyAsync();

            if (!CheckAlreadyAdded)
            {
                var query = await (from c in _dbContext.StudentCourseAllocationTb
                                   join s in _dbContext.LoginTb on c.LoginId equals s.LoginId
                                   join a in _dbContext.CoursesTb on c.CourseId equals a.CourseId
                                   join f in _dbContext.SesstionTB on c.SesstionId equals f.SesstionId
                                   where c.CourseId == CourseId && s.DepartmentId == DeparmentId && c.SesstionId == CurrentSessionId
                                   select new StudentCourseAllocationViewModel
                                   {
                                       CourseName = a.CourseName,
                                       Email = s.Email,
                                       StudentName = s.FirstName + " " + s.LastName,
                                       CourseId = CourseId,
                                       IsCourseActive = c.IsCourseActive,
                                       LoginId = s.LoginId,
                                       SesstionId = c.SesstionId,
                                       SesstionName = f.Session,
                                       DepartmentId = s.DepartmentId,
                                       StudentCourseAllocationId = c.StudentCourseAllocationId
                                   }).ToListAsync();

                return query;
            }
            else
            {
                return new List<StudentCourseAllocationViewModel>();
            }
        }

        public async Task<List<StudentCourseAllocationViewModel>> GetStudentToBeUpgradeAsync(int CourseId, int DeparmentId)
        {
            var query = await (from c in _dbContext.StudentCourseAllocationTb
                               join s in _dbContext.LoginTb on c.LoginId equals s.LoginId
                               join a in _dbContext.CoursesTb on c.CourseId equals a.CourseId
                               join f in _dbContext.SesstionTB on c.SesstionId equals f.SesstionId
                               where c.CourseId == CourseId && s.DepartmentId == DeparmentId && c.IsCourseActive == true
                               select new StudentCourseAllocationViewModel
                               {
                                   CourseName = a.CourseName,
                                   Email = s.Email,
                                   StudentName = s.FirstName + " " + s.LastName,
                                   CourseId = CourseId,
                                   IsCourseActive = c.IsCourseActive,
                                   LoginId = s.LoginId,
                                   SesstionId = c.SesstionId,
                                   SesstionName = f.Session,
                                   DepartmentId = s.DepartmentId,
                                   StudentCourseAllocationId = c.StudentCourseAllocationId
                               }).ToListAsync();

            return query;
        }

        public async Task<string> SaveStudentAttandenceAsync(List<StudentAttandenceTb> valueList)
        {
            foreach (var q in valueList)
            {
                StudentAttandenceTb studentAttandenceTb = new StudentAttandenceTb();
                studentAttandenceTb.SessionId = q.SessionId;
                studentAttandenceTb.DeparmentId = q.DeparmentId;
                studentAttandenceTb.TotalLectures = q.TotalLectures;
                studentAttandenceTb.Presents = q.Presents;
                studentAttandenceTb.CreateOn = DateTime.Now;
                studentAttandenceTb.LoginId = q.LoginId;
                studentAttandenceTb.CourseId = q.CourseId;
                await _dbContext.StudentAttandenceTb.AddAsync(studentAttandenceTb);
                await _dbContext.SaveChangesAsync();
            }

            return "Save Successfully";
        }

        public async Task<string> SaveStudentGradesAsync(List<StudentCourseGradesTb> valueList)
        {
            foreach (var q in valueList)
            {
                StudentCourseGradesTb student = new StudentCourseGradesTb();
                student.LoginId = q.LoginId;
                student.DeparmentId = q.DeparmentId;
                student.CourseId = q.CourseId;
                student.TotalMarks = q.TotalMarks;
                student.SessionId = q.SessionId;
                student.ObtainMarks = q.ObtainMarks;
                student.Percentage = q.Percentage;
                student.TeacherId = q.TeacherId;
                student.CreatedOn = DateTime.Now;
                await _dbContext.StudentCourseGradesTb.AddAsync(student);
                await _dbContext.SaveChangesAsync();
            }

            return "Save Successfully";
        }

        public async Task<string> UpgradStudentToNextCourseAsync(List<StudentCourseAllocationTb> valueList)
        {
            foreach (var item in valueList)
            {
                bool IsStudentGetGrades = await GetStudentGradesAsync(item.LoginId, item.CourseId);
                bool IsStudentGetAttendance = await GetStudentAttendanceAsync(item.LoginId, item.CourseId);
                int NextCourseId = await GetNexCourseIdAsync(item.LoginId, item.CourseId);

                if (IsStudentGetAttendance && IsStudentGetGrades)
                {
                    var DeActiveLastCourse = await _dbContext.StudentCourseAllocationTb
                        .Where(x => x.LoginId == item.LoginId && x.CourseId == item.CourseId)
                        .FirstOrDefaultAsync();

                    if (DeActiveLastCourse != null)
                    {
                        DeActiveLastCourse.IsCourseActive = false;
                        await _dbContext.SaveChangesAsync();
                    }

                    if (NextCourseId != 0)
                    {
                        StudentCourseAllocationTb student = new StudentCourseAllocationTb();
                        student.LoginId = item.LoginId;
                        student.CourseId = NextCourseId;
                        student.SesstionId = item.SesstionId;
                        student.IsCourseActive = true;

                        await _dbContext.StudentCourseAllocationTb.AddAsync(student);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return "Not eligible for update";
                    }
                }
                else
                {
                    return "Not eligible for update";
                }
            }

            return "Upgradation has been done!";
        }

        private async Task<int> GetNexCourseIdAsync(int loginId, int courseId)
        {
            var DeparmentId = await _dbContext.LoginTb
                .Where(x => x.LoginId == loginId)
                .Select(x => x.DepartmentId)
                .FirstOrDefaultAsync();

            var AllCouseOfDeparment = await _dbContext.CoursesTb
                .Where(x => x.DeparmentId == DeparmentId && x.IsDeleted == false)
                .OrderBy(x => x.CourseId)
                .ToListAsync();

            var currentIndex = AllCouseOfDeparment.FindIndex(x => x.CourseId == courseId);

            if (currentIndex >= 0 && currentIndex < AllCouseOfDeparment.Count - 1)
            {
                if (currentIndex == 0)
                {
                    currentIndex = 1;
                }
                else
                {
                    currentIndex = currentIndex + 1;
                }
                currentIndex = currentIndex + 1;
                return currentIndex;
            }
            else
            {
                return 0;
            }
        }

        private async Task<bool> GetStudentGradesAsync(int loginId, int courseId)
        {
            var query = await _dbContext.StudentCourseGradesTb
                .Where(x => x.LoginId == loginId && x.CourseId == courseId)
                .FirstOrDefaultAsync();

            double TotalMarks = query.TotalMarks;
            double ObtainMarks = query.ObtainMarks;
            double Percentage = ObtainMarks / TotalMarks * 100;

            return Percentage >= 80;
        }

        private async Task<bool> GetStudentAttendanceAsync(int loginId, int courseId)
        {
            var query = await _dbContext.StudentAttandenceTb
                .Where(x => x.LoginId == loginId && x.CourseId == courseId)
                .FirstOrDefaultAsync();

            double TotalLectures = query.TotalLectures;
            double TotalPresence = query.Presents;
            double TotalAbsence = TotalLectures - TotalPresence;
            double Percentage = TotalAbsence / TotalLectures * 100;

            return Percentage <= 20;
        }
    }
}
