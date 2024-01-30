using Core.domain.Models;
using Core.domain.Models.Models;
using Core.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMSForStudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IConfiguration _config;

        public AccountController(IAdminService adminService, IConfiguration config)
        {
            this._adminService = adminService;
            this._config = config;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _adminService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAdminAndTeacher")]
        public async Task<IActionResult> GetAdminAndTeacher()
        {
            var result = await _adminService.GetAdminAndTeacherAsync();
            return Ok(result);
        }

        [EnableCors("default")]
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _adminService.GetDepartmentsListAsync();
            return Ok(result);
        }

        [EnableCors("default")]
        [HttpGet]
        [Route("GetActiveSession")]
        public async Task<IActionResult> GetActiveSession()
        {
            var result = await _adminService.GetActiveSessionAsync();
            return Ok(result);
        }

        [EnableCors("default")]
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] LoginViewModel value)
        {
            var response = await _adminService.RegisterUserAsync(value);
            return new JsonResult(response);
        }

        [EnableCors("default")]
        [AllowAnonymous]
        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginModel value)
        {
            var user = await _adminService.LoginUserAsync(value);
            if (user != null)
            {
                var jwt = _config.GetSection("Jwt").Get<Jwt>();
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("LoginId", user.LoginId.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    signingCredentials: signIn,
                    expires: DateTime.Now.AddMinutes(20)
                );

                user.Token = new JwtSecurityTokenHandler().WriteToken(token);
                user.LoginId = user.LoginId;
            }

            return new JsonResult(user);
        }
    }
}
