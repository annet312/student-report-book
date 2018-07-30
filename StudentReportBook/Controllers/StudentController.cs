using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.ViewModel;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using System.Security.Claims;

namespace StudentReportBook.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Student")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService studentService;
        private readonly IMapper mapper;
        private readonly IMarkService markService;
        private readonly IGradeBookService gradeBookService;
        private readonly ITeacherService teacherService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StudentController(IMapper mapper, IStudentService studentService, IMarkService markService, IGradeBookService gradeBookService, ITeacherService teacherService, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.studentService = studentService;
            this.markService = markService;
            this.gradeBookService = gradeBookService;
            this.teacherService = teacherService;
            this.httpContextAccessor = httpContextAccessor;
        }
        

        [HttpGet]
       
        public IActionResult GetMyGradeBook()
        {
            //int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
            //userId for testing
            GradeBook gradeBook = gradeBookService.GetMyMarks();
            var result = mapper.Map<GradeBookViewModel>(gradeBook);
            
            return new OkObjectResult(result);
        }
    }
}