using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentReportBook.Models.Entities;
using StudentReportBookBLL.Services.Interfaces;

namespace StudentReportBook.Controllers
{
    //[Authorize(Policy = "Student")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService studentService;
        private readonly IMapper mapper;
        private readonly IMarkService markService;
        private readonly IGradeBookService gradeBookService;

        public StudentController(IMapper mapper, IStudentService studentService, IMarkService markService, IGradeBookService gradeBookService)
        {
            this.mapper = mapper;
            this.studentService = studentService;
            this.markService = markService;
            this.gradeBookService = gradeBookService;
        }

        //GET api/student/getMygradeBook
        [HttpGet]
        public IActionResult GetMyGradeBook()
        {
            var students = studentService.GetStudents(7);
            markService.AddMark(3, 3, "6b4cc5ee-7535-4e6f-9723-51469714a96b", 1);
            var mark = gradeBookService.GetMyMarks("2daff0cc-533e-45a0-b30e-0ad6f77c92f9");
            return new OkObjectResult(mark);
        }
    }
}