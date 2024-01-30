using Core.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LMSForStudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("GetStudentAttendance")]
        public async Task<IActionResult> GetStudentAttendance(int loginId)
        {
            var result = await _studentService.GetStudentAttendanceAsync(loginId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetStudentGrades")]
        public async Task<IActionResult> GetStudentGrades(int loginId)
        {
            var result = await _studentService.GetStudentGradesAsync(loginId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetStudentRoomCourseAndTeacherAllocation")]
        public async Task<IActionResult> GetStudentRoomCourseAndTeacherAllocation(int CourseId)
        {
            var result = await _studentService.GetStudentRoomCourseAndTeacherAllocationAsync(CourseId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetStudentCurrentCourse")]
        public async Task<IActionResult> GetStudentCurrentCourse(int LoginId)
        {
            var result = await _studentService.GetStudentCurrentCourseAsync(LoginId);
            return Ok(result);
        }
    }
}
