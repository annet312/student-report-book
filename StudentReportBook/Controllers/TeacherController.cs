
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICurrentTeacherService currentTeacherService;
        private readonly IMapper mapper;

        public TeacherController(IMapper mapper,
                                    ICurrentTeacherService currentTeacherService)
        {
            this.mapper = mapper;
            this.currentTeacherService = currentTeacherService;
        }

        /// <summary>
        /// get subjects that this teacher has in workload
        /// </summary>
        /// <returns>subjects</returns>
        [HttpGet]
        public IActionResult GetSubjectsForCurrentTeacher( )
        {
            IEnumerable<SubjectBll> subjects = currentTeacherService.GetSubjectsForCurrentTeacher();
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
            IEnumerable<FacultyBll> faculties = currentTeacherService.GetFacultiesForCurrentTeacher(subjectId);
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
            IEnumerable<GroupBll> groups = currentTeacherService.GetGroupsForCurrentTeacher(facultyId,subjectId);
            IEnumerable<GroupViewModel> groupViewModels = mapper.Map<IEnumerable<GroupViewModel>>(groups);

            return new OkObjectResult(groupViewModels);
        }

        [HttpGet]
        public IActionResult GetTerms(int subjectId, int groupId)
        {
            int[] terms = currentTeacherService.GetTermsForCurrentTeacher(groupId, subjectId);

            return new OkObjectResult(terms);
        }

        [HttpGet]
        public IActionResult GetStudents(int groupId, int subjectId)
        {
            IEnumerable<MarkOfStudent> students = currentTeacherService.GetStudentsWithMarks(groupId, subjectId);
            IEnumerable<MarkOfStudentViewModel> studentViewModels = mapper.Map<IEnumerable<MarkOfStudentViewModel>>(students);

            return new OkObjectResult(studentViewModels);
        }

        [HttpPost]
        public IActionResult EditMark([FromBody]EditMarkViewModel model)
        {
            MarkBll mark = currentTeacherService.EditMarkByCurrentTeacher(model.studentId, model.subjectId, model.term, model.grade);

            MarkViewModel markVM = mapper.Map<MarkViewModel>(mark);
            return new OkObjectResult(markVM);
        }
    }
}