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

        private readonly IMapper mapper;
        private readonly IGradeBookService gradeBookService;

        public StudentController(IMapper mapper, IGradeBookService gradeBookService)
        {
            this.mapper = mapper;
            this.gradeBookService = gradeBookService;
        }
        

        [HttpGet]
       
        public IActionResult GetMyGradeBook()
        {
         
            GradeBook gradeBook = gradeBookService.GetMyMarks();
            var result = mapper.Map<GradeBookViewModel>(gradeBook);
            
            return new OkObjectResult(result);
        }
    }
}