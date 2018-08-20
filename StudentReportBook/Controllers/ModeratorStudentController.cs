using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using StudentReportBook.Helpers;
using StudentReportBook.Models.Entities;
using StudentReportBook.ViewModel;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;


namespace StudentReportBook.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Moderator")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModeratorStudentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IStudentService studentService;

        public ModeratorStudentController(IMapper mapper,  IStudentService studentService)
        {
            this.mapper = mapper;
            this.studentService = studentService;
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
        public IActionResult SetGroupForStudent(int studentId, int groupId, string studentCard)
        {
            try
            {
                studentService.SetGroupForStudent(studentId, groupId, studentCard);
            }
            catch(ArgumentException e)
            {
                return new BadRequestObjectResult(Errors.AddErrorToModelState("setstudentgroup_failure", e.Message, ModelState));
            }
            catch(InvalidOperationException e)
            {
                return new BadRequestObjectResult(Errors.AddErrorToModelState("setstudentgroup_failure", e.Message, ModelState));
            }
            return new OkObjectResult(true);
        }
    }
}