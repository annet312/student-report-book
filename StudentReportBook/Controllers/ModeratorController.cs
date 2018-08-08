
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.Models.Entities;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using System.Collections.Generic;

namespace StudentReportBook.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Moderator")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITeacherService teacherService;
        private readonly IStudentService studentService;

        public ModeratorController(IMapper mapper, ITeacherService teacherService, IStudentService studentService)
        {
            this.mapper = mapper;
            this.teacherService = teacherService;
            this.studentService = studentService;
        }

        [HttpGet]
        public IEnumerable<Teacher> GetTeachers()
        {
            IEnumerable<TeacherBll> teachersbll = teacherService.GetAllTeachers();
            IEnumerable<Teacher> teachers = mapper.Map<IEnumerable<Teacher>>(teachersbll);
            return teachers;
        }

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            IEnumerable<StudentBll> studentsbll = studentService.GetStudentWithoutGroup();
            IEnumerable<Student> students = mapper.Map<IEnumerable<Student>>(studentsbll);
            return students;
        }
    }
}