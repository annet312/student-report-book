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
using StudentReportBookBLL.Models;
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
        private readonly ITeacherService teacherService;

        public StudentController(IMapper mapper, IStudentService studentService, IMarkService markService, IGradeBookService gradeBookService, ITeacherService teacherService)
        {
            this.mapper = mapper;
            this.studentService = studentService;
            this.markService = markService;
            this.gradeBookService = gradeBookService;
            this.teacherService = teacherService;
        }

        //GET api/student/getMygradeBook
        [HttpGet]
        public IActionResult GetMyGradeBook()
        {
            IEnumerable<StudentBll> students = null;

            students = studentService.GetStudents(7);

            

            var stud = mapper.Map<IEnumerable<Student>>(students);

            GradeBook gradeBook = gradeBookService.GetMyMarks("2daff0cc-533e-45a0-b30e-0ad6f77c92f9");
            ;
            // var teacher = teacherService.GetTeacher("6b4cc5ee-7535-4e6f-9723-51469714a96b");
            //var res = teacherService.GetGroups(1, "6b4cc5ee-7535-4e6f-9723-51469714a96b", 1);         //markService.AddMark(3, 3, "6b4cc5ee-7535-4e6f-9723-51469714a96b", 1);
            //var mark = gradeBookService.GetMyMarks("2daff0cc-533e-45a0-b30e-0ad6f77c92f9");
            return new OkObjectResult(stud);
        }
    }
}