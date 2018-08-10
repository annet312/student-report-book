using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.Models.Entities;
using StudentReportBook.ViewModel;
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
        public IEnumerable<Student> GetStudentsWithoutGroup()
        {
            IEnumerable<StudentBll> studentsbll = studentService.GetStudentWithoutGroup();
            IEnumerable<Student> students = mapper.Map<IEnumerable<Student>>(studentsbll);
            return students;
        }

        [HttpGet]
        public IEnumerable<FacultyWithGroupsViewModel> GetAllFaculties()
        {

            IEnumerable<FacultyBll> facultiesbll = studentService.GetAllFaculties();
            IEnumerable<FacultyWithGroupsViewModel> faculties = mapper.Map<IEnumerable<FacultyWithGroupsViewModel>>(facultiesbll);
            return faculties;
        }

        [HttpGet]
        public IEnumerable<Student> GetGroupsOfFaculty( int facultyId)
        {
            IEnumerable<StudentBll> studentsbll = studentService.GetStudentWithoutGroup();
            IEnumerable<Student> students = mapper.Map<IEnumerable<Student>>(studentsbll);
            return students;
        }


        [HttpGet]
        public IActionResult SetGroupForStudent(int studentId, int groupId)
        {
            studentService.SetGroupForStudent(studentId, groupId);
            return new OkObjectResult(true);
        }

        [HttpGet]
        public IActionResult GetTeacherWorkloads(int teacherId)
        {
            IEnumerable<TeachersWorkloadBll> twBll= teacherService.GetTeachersWorkloads(teacherId);
            if (twBll == null)
                return new OkObjectResult(twBll);

            IEnumerable<TeacherWorkloadViewModel> tws = mapper.Map<IEnumerable<TeacherWorkloadViewModel>>(twBll);
            return new OkObjectResult(tws);
        }

    }
}