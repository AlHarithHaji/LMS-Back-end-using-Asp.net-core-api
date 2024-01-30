using Core.Domain.Models;
using Core.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LMSForStudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        [Route("SaveStudentAttandence")]
        public IActionResult SaveStudentAttandence(List<StudentCourseAllocationViewModel> valueList)
        {
            var studentAttendanceList = new List<StudentAttandenceTb>();
            foreach (var q in valueList)
            {
                studentAttendanceList.Add(new StudentAttandenceTb
                {
                    CourseId = q.CourseId,
                    DeparmentId = (int)q.DepartmentId,
                    LoginId = q.LoginId,
                    Presents = (int)q.Presents,
                    TotalLectures = (int)q.TotalLectures,
                    SessionId = q.SesstionId,
                    CreateOn = DateTime.Now
                });
            }
            return Ok(_teacherService.SaveStudentAttandenceAsync(studentAttendanceList));
        }

        [HttpPost]
        [Route("SaveStudentGrades")]
        public IActionResult SaveStudentGrades(List<StudentCourseAllocationViewModel> valueList)
        {
            var studentGradesList = new List<StudentCourseGradesTb>();
            foreach (var q in valueList)
            {
                var percentage = (double)q.ObtainMarks / (double)q.TotalMarks * 100;
                studentGradesList.Add(new StudentCourseGradesTb
                {
                    CourseId = q.CourseId,
                    DeparmentId = (int)q.DepartmentId,
                    LoginId = q.LoginId,
                    ObtainMarks = (double)q.ObtainMarks,
                    TotalMarks = (double)q.TotalMarks,
                    SessionId = q.SesstionId,
                    CreatedOn = DateTime.Now,
                    Percentage = Math.Round(percentage, 2)
                });
            }
            return Ok(_teacherService.SaveStudentGradesAsync(studentGradesList));
        }

        [HttpPost]
        [Route("UpgradStudentToNextCourse")]
        public IActionResult UpgradStudentToNextCourse(List<StudentCourseAllocationViewModel> valueList)
        {
            var studentAllocationList = new List<StudentCourseAllocationTb>();
            foreach (var q in valueList)
            {
                studentAllocationList.Add(new StudentCourseAllocationTb
                {
                    CourseId = q.CourseId,
                    LoginId = q.LoginId,
                    SesstionId = q.SesstionId,
                });
            }
            return Ok(_teacherService.UpgradStudentToNextCourseAsync(studentAllocationList));
        }

        [HttpGet]
        [Route("GetStudentsAttendance")]
        public IActionResult GetStudentsAttendance(int CourseId, int DeparmentId)
        {
            return Ok(_teacherService.GetStudentsAttendanceAsync(CourseId, DeparmentId));
        }

        [HttpGet]
        [Route("GetStudentsGrade")]
        public IActionResult GetStudentsGrade(int CourseId, int DeparmentId)
        {
            return Ok(_teacherService.GetStudentsGradeAsync(CourseId, DeparmentId));
        }

        [HttpGet]
        [Route("GetStudentToBeUpgrade")]
        public IActionResult GetStudentToBeUpgrade(int CourseId, int DeparmentId)
        {
            return Ok(_teacherService.GetStudentToBeUpgradeAsync(CourseId, DeparmentId));
        }
    }
}
