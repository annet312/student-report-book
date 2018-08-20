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
    public class ModeratorTeacherController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITeacherService teacherService;
        private readonly IStudentService studentService;

        public ModeratorTeacherController(IMapper mapper, ITeacherService teacherService, IStudentService studentService)
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
        public IEnumerable<GroupViewModel> GetAllGroups()
        {
            IEnumerable<GroupBll> groupsbll = studentService.GetAllGroups();
            IEnumerable<GroupViewModel> groups = mapper.Map<IEnumerable<GroupViewModel>>(groupsbll);
            return groups;
        }

        [HttpGet]
        public IActionResult ChangeGroup(int teacherWorkloadId, int groupId)
        {
            try
            {
                teacherService.ChangeGroup(teacherWorkloadId, groupId);
            }
            catch (ArgumentException e)
            {
                return new BadRequestObjectResult(Errors.AddErrorToModelState("changegroup_failure", e.Message, ModelState));
            }
            return new OkObjectResult(true);
        }

        [HttpGet]
        public IActionResult ChangeSubject(int teacherWorkloadId, int subjectId)
        {
            try
            {
                teacherService.ChangeSubject(teacherWorkloadId, subjectId);
            }
            catch (ArgumentException e)
            {
                return new BadRequestObjectResult(Errors.AddErrorToModelState("changesubject_failure", e.Message, ModelState));
            }
            return new OkObjectResult(true);
        }

        [HttpGet]
        public IActionResult GetTeacherWorkloads(int teacherId)
        {
            IEnumerable<TeachersWorkloadBll> twBll = teacherService.GetTeachersWorkloads(teacherId);
            if (twBll == null)
                return new OkObjectResult(twBll);

            IEnumerable<TeacherWorkloadViewModel> tws = mapper.Map<IEnumerable<TeacherWorkloadViewModel>>(twBll);
            return new OkObjectResult(tws);
        }

        [HttpGet]
        public IActionResult GetAllSubjects()
        {
            IEnumerable<SubjectBll> subjects = teacherService.GetAllSubjects();
            if (subjects == null)
                return new OkObjectResult(subjects);

            IEnumerable<SubjectViewModel> subjectViews = mapper.Map<IEnumerable<SubjectViewModel>>(subjects);
            return new OkObjectResult(subjectViews);
        }

        [HttpGet]
        public IActionResult ChangeTerm(int teacherWorkloadId, int term)
        {
            try
            {
                teacherService.ChangeTerm(teacherWorkloadId, term);
            }
            catch (ArgumentException e)
            {
                return new BadRequestObjectResult(Errors.AddErrorToModelState("changeterm_failure", e.Message, ModelState));
            }
            return new OkObjectResult(true);
        }

        [HttpPost]
        public IActionResult AddWorkload([FromBody]AddWorkloadViewModel model)
        {
            TeachersWorkloadBll tws;
            try
            {
                tws = teacherService.AddWorkload(model.TeacherId, model.SubjectId, model.GroupId, model.Term);
            }
            catch (ArgumentException e)
            {
                return new BadRequestObjectResult(Errors.AddErrorToModelState("addworkload_failure", e.Message, ModelState));
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(Errors.AddErrorToModelState("addworkload_failure", e.Message, ModelState));
            }

            TeacherWorkloadViewModel res = mapper.Map<TeacherWorkloadViewModel>(tws);
            return new OkObjectResult(res);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkload(int id)
        {
            try
            {
                teacherService.DeleteWorkload(id);
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(Errors.AddErrorToModelState("deleteworkload_failure", e.Message, ModelState));
            }
            return new OkObjectResult(true);
        }
    }
}