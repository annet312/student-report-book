using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.ViewModel;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;


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

        public StudentController(IMapper mapper, IStudentService studentService, IMarkService markService, IGradeBookService gradeBookService, ITeacherService teacherService)
        {
            this.mapper = mapper;
            this.studentService = studentService;
            this.markService = markService;
            this.gradeBookService = gradeBookService;
            this.teacherService = teacherService;
        }
        

        [HttpGet]
       
        public IActionResult GetMyGradeBook()
        {
            var r = Request.Headers["HeaderAuthorization"];
            //int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
            //userId for testing
            GradeBook gradeBook = gradeBookService.GetMyMarks("2daff0cc-533e-45a0-b30e-0ad6f77c92f9");
            var result = mapper.Map<GradeBookViewModel>(gradeBook);
            
            return new OkObjectResult(result);
        }
    }
}