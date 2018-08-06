
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.Models.Entities;
using StudentReportBook.ViewModel;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using System.Collections.Generic;

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
        /// get subjects that this teacher has in workload
        /// </summary>
        /// <returns>subjects</returns>
        [HttpGet]
        public IActionResult GetSubjectsForCurrentTeacher( )
        {
            IEnumerable<SubjectBll> subjects = studentService.GetSubjectsForCurrentTeacher();
           
            return new OkObjectResult(subjects);
        }

        /// <summary>
        /// get faculties where this teacher work with pointed subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFaculties(int subjectId)
        {
            IEnumerable<FacultyBll> faculties = studentService.GetFacultiesForCurrentTeacher(subjectId);
            IEnumerable<FacultyViewModel> facultyViewModels = mapper.Map < IEnumerable<FacultyViewModel>>(faculties);
            return new OkObjectResult(facultyViewModels);
        }
        /// <summary>
        /// get groups where current teacher work with filtered subject and faculty
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns>groups</returns>
        [HttpGet]
        public IActionResult GetGroups(int subjectId, int facultyId)
        {
            IEnumerable<GroupBll> groups = studentService.GetGroupsForCurrentTeacher(facultyId,subjectId);
            IEnumerable<GroupViewModel> groupViewModels = mapper.Map<IEnumerable<GroupViewModel>>(groups);
            return new OkObjectResult(groupViewModels);
        }

        [HttpGet]
        public IActionResult GetStudents(int groupId)
        {
            IEnumerable<StudentBll> students = studentService.GetStudents(groupId);

            IEnumerable<StudentViewModel> studentViewModels = mapper.Map<IEnumerable<StudentViewModel>>(students);

            return new OkObjectResult(studentViewModels);
        }
    }
}