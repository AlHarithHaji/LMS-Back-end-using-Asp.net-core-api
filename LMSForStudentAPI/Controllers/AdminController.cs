
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ClosedXML.Excel;
using LMSWebAPI.Utilities;
using Core.Services.Services;
using Core.Domain.Utilities;
using Core.domain.Models;
using Core.Domain.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LMSForStudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "AdminPolicy")]

    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            this._adminService = adminService;
        }

        // Get All Register Users
        [HttpGet]
        [Route("GetNewRegisterUser")]

        public async Task<IActionResult> GetNewRegisterUser()
        {
            var result = await _adminService.GetNewRegisterUserAsync();
            return Ok(result);
        }

        // Get: Active user
        [HttpGet]
        [Route("ActivateUser/{LoginId}")]
        public async Task<IActionResult> ActivateUser(int LoginId)
        {
            var result = await _adminService.ActiveUserAsync(LoginId);
            return Ok(result);
        }

        // Get: Active user
        [HttpGet]
        [Route("DeActiveUser/{LoginId}")]
        public async Task<IActionResult> DeActiveUser(int LoginId)
        {
            var result = await _adminService.DeActiveUserAsync(LoginId);
            return Ok(result);
        }

        // Get: Active user
        [HttpGet]
        [Route("GetExcelOfRegisterStudent")]
        public async Task<IActionResult> GetExcelOfRegisterStudent(int DeparmentId)
        {
            var response = await _adminService.GetExcelOfRegisterStudentAsync(DeparmentId);

            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = ConvertToDataTable.BuildDataTable(response);
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        [HttpGet]
        [Route("GetRegisterStudent")]
        public async Task<IActionResult> GetRegisterStudent(int DeparmentId)
        {
            var result = await _adminService.GetExcelOfRegisterStudentAsync(DeparmentId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent(int DeparmentId)
        {
            var result = await _adminService.GetAllStudentAsync(DeparmentId);
            return Ok(result);
        }

        // Get: Active user
        [HttpGet]
        [Route("SendEmailToRegisterStudents")]
        public async Task<IActionResult> SendEmailToRegisterStudents(int DeparmentId)
        {
            var result = await _adminService.SendEmailToRegisterStudentAsync(DeparmentId);
            return Ok(result);
        }

        #region Manage Courses

        [HttpGet]
        [Route("GetAllCourse")]
        public async Task<IActionResult> GetAllCourse()
        {
            var result = await _adminService.GetAllCourseAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveCourse")]
        public async Task<IActionResult> SaveCourse([FromBody] CourseViewModel value)
        {
            var result = await _adminService.SaveCourseAsync(value);
            return Ok(result);
        }

        [HttpGet]
        [Route("DeleteCourse/{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var result = await _adminService.DeleteCourseAsync(courseId);
            return Ok(result);
        }

        #endregion

        #region Manang Teachers

        [HttpGet]
        [Route("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await _adminService.GetAllTeachersAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveTeacher")]
        public async Task<IActionResult> SaveTeacher([FromBody] TeacherTb value)
        {
            var result = await _adminService.SaveTeacherAsync(value);
            return Ok(result);
        }

        [HttpGet]
        [Route("DeleteTeacher/{TeacherId}")]
        public async Task<IActionResult> DeleteTeacher(int TeacherId)
        {
            var result = await _adminService.DeleteTeacherAsync(TeacherId);
            return Ok(result);
        }

        #endregion

        #region Manang ClassRoom

        [HttpGet]
        [Route("GetAllClassRoom")]
        public async Task<IActionResult> GetAllClassRoom()
        {
            var result = await _adminService.GetAllClassRoomAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveClassRoom")]
        public async Task<IActionResult> SaveClassRoom([FromBody] ClassRoomTb value)
        {
            var result = await _adminService.SaveClassRoomAsync(value);
            return Ok(result);
        }

        [HttpGet]
        [Route("DeleteClassRoom/{ClassRoomid}")]
        public async Task<IActionResult> DeleteClassRoom(int ClassRoomid)
        {
            var result = await _adminService.DeleteClassRoomAsync(ClassRoomid);
            return Ok(result);
        }

        #endregion

        #region AllocationRoomandTeacher

        [HttpPost]
        [Route("RoomAndTeacherAllocation")]
        public async Task<IActionResult> RoomAndTeacherAllocation([FromBody] AllocationTeacherTb value)
        {
            var result = await _adminService.RoomAndTeacherAllocationAsync(value);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetRoomAndTeacherAllocation")]
        public async Task<IActionResult> GetRoomAndTeacherAllocation()
        {
            var result = await _adminService.GetRoomAndTeacherAllocationAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("DeleteRoomAndTeacherAllocation/{Id}")]
        public async Task<IActionResult> DeleteRoomAndTeacherAllocation(int Id)
        {
            var result = await _adminService.DeleteRoomAndTeacherAllocationAsync(Id);
            return Ok(result);
        }

        #endregion
    }
}
