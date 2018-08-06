using AutoMapper;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentReportBookBLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork db;
        private readonly IUserService userService;

        public StudentService(IMapper mapper, IUnitOfWork db, IUserService userService)
        {
            this.mapper = mapper;
            this.db = db;
            this.userService = userService;
        }
        private TeacherBll GetCurrentTeacherId()
        {
            var userName = userService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("userName is not available", userName);

            Teacher teacher = db.Teachers.Get(s => s.Identity.UserName == userName).SingleOrDefault();
            if (teacher == null) throw new ArgumentException("This teacher does not exists", userName);

            var teacherBll = mapper.Map<TeacherBll>(teacher);
            return teacherBll;
        }

        private IEnumerable<TeachersWorkloadBll> GetTWOfCurrentTeacher()
        {
            TeacherBll teacher = GetCurrentTeacherId();

            var teacherWorkloads = db.TeachersWorkloads.Get(tw => tw.TeacherId == teacher.Id);
            IEnumerable<TeachersWorkloadBll> workloadBlls = mapper.Map<IEnumerable<TeachersWorkloadBll>>(teacherWorkloads);

            return workloadBlls;
        }
        public IEnumerable<SubjectBll> GetSubjectsForCurrentTeacher()
        {
            var tws = GetTWOfCurrentTeacher();
            if (tws.Any())
                return null;
            IEnumerable<SubjectBll> subjects = tws.Select(tw => tw.Subject);

            return subjects;
        }

        public IEnumerable<FacultyBll> GetFaculties()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupBll> GetGroups(int facultyId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentBll> GetStudents(int groupId)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<FacultyBll> GetFaculties()
        //{
        //    //TeacherBll teacher = GetCurrentTeacherId();

        //    //IEnumerable<TeachersWorkload> tw = db.TeachersWorkloads.Get(tw => tw.TeacherId == teacher.Id);
        //    //IEnumerable<Faculty> faculties = GetFaculties(teacher.)
        //    //IEnumerable<FacultyBll> facultiesBll = mapper.Map<IEnumerable<FacultyBll>>(faculties);

        //    //return facultiesBll;
        //}

        //public IEnumerable<FacultyBll> GetFaculties(int teacherWorkloadsId)
        //{
        //    IEnumerable<GroupBll> groups = GetGroups(teacherWorkloadsId);

        //    IEnumerable<FacultyBll> faculties = groups.Select(g => g.Faculty);

        //    return faculties;

        //}

        //private IEnumerable<GroupBll> GetGroups(int teachersWorkloadId)
        //{
        //    IEnumerable<TeachersWorkloadBll> teachersWorkloads = db.TeachersWorkloads.Get(tw => tw.Id == teachersWorkloadId);

        //    if (!teachersWorkloads.Any()) return null;

        //    IEnumerable<Group> groups = teachersWorkloads.Select(tw => tw.Group);
        //    IEnumerable<GroupBll> groupsBll = mapper.Map<IEnumerable<GroupBll>>(groups);

        //    return groupsBll;
        //}

        //    IEnumerable<GroupBll> IStudentService.GetGroups(int facultyId)
        //    {
        //        IEnumerable<Group> groups = db.Groups.Get(g => g.Faculty.Id == facultyId);
        //        IEnumerable<GroupBll> groupsBll = mapper.Map<IEnumerable<GroupBll>>(groups);
        //        return groupsBll;
        //    }

        //    //public  IEnumerable<GroupBll> GetGroups(int facultyId, int teachersWorkloadId)
        //    //{
        //    //    IEnumerable<GroupBll> groups = this.GetGroups(teachersWorkloadId).Where(g => g.Faculty.Id == facultyId);

        //    //    return groups;
        //    //}

        //    public IEnumerable<StudentBll> GetStudents(int groupId)
        //    {
        //        IEnumerable<Student> students = db.Students.Get(s => s.Group.Id == groupId);
        //        IEnumerable<StudentBll> studentsBll = mapper.Map<IEnumerable<StudentBll>>(students);
        //        return studentsBll;
        //    }
    }
}
