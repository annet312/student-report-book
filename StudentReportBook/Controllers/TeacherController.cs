
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.Models.Entities;
using StudentReportBook.ViewModel;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using System;
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

        public TeacherController(IMapper mapper,
                                    IStudentService studentService)
        {
            this.mapper = mapper;
            this.studentService = studentService;
        }

        /// <summary>
        /// get subjects that this teacher has in workload
        /// </summary>
        /// <returns>subjects</returns>
        [HttpGet]
        public IActionResult GetSubjectsForCurrentTeacher( )
        {
            IEnumerable<SubjectBll> subjects = studentService.GetSubjectsForCurrentTeacher();
            IEnumerable<SubjectViewModel> subjectViews = mapper.Map<IEnumerable<SubjectViewModel>>(subjects);
           
            return new OkObjectResult(subjectViews);
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
        public IActionResult GetTerms(int subjectId, int groupId)
        {
            int[] terms = studentService.GetTermsForCurrentTeacher(groupId, subjectId);

            return new OkObjectResult(terms);
        }

        [HttpGet]
        public IActionResult GetStudents(int groupId, int subjectId)
        {
            IEnumerable<MarkOfStudent> students = studentService.GetStudentsWithMarks(groupId, subjectId);
            IEnumerable<MarkOfStudentViewModel> studentViewModels = mapper.Map<IEnumerable<MarkOfStudentViewModel>>(students);
            return new OkObjectResult(studentViewModels);
        }

        [HttpPost]
        public IActionResult EditMark([FromBody]EditMarkViewModel model)
        {
            bool IfEdit = studentService.EditMarkByCurrentTeacher(model.studentId, model.subjectId, model.term, model.grade);
            
            return new OkObjectResult(IfEdit);
        }
        
    }
}