
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentReportBookBLL.Services.Interfaces;

namespace StudentReportBook.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IMapper mapper;
        private readonly IMarkService markService;
        private readonly ITeacherService teacherService;

        public TeacherController(IMapper mapper,IStudentService studentService, ITeacherService teacherService, IMarkService markService)
        {
            this.mapper = mapper;
            this.studentService = studentService;
            this.teacherService = teacherService;
            this.markService = markService;
        }
        /// <summary>
        /// get faculties where this teacher work
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSubjectsForCurrentTeacher( )
        {
            var subjects = studentService.GetSubjectsForCurrentTeacher();
          
            return new OkObjectResult(subjects);
        }
    }
}