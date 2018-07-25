using AutoMapper;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentReportBookBLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork db;
        private readonly IStudentService studentService;
        public TeacherService(IMapper mapper, IUnitOfWork db,IStudentService studentservice)
        {
            this.mapper = mapper;
            this.db = db;
            this.studentService = studentService;
        }
        public IEnumerable<TeacherBll> GetAllTeachers()
        {
            IEnumerable<Teacher> results;

            results = db.Teachers.GetAll();
            IEnumerable<TeacherBll> resultsBll = mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherBll>>(results);

            return resultsBll;
        }


        public IEnumerable<FacultyBll> GetFaculties(string userId, int subjectId)
        {
            TeacherBll teacher = GetTeacher(userId);
            IEnumerable<TeachersWorkload> techersWorkload = db.TeachersWorkloads.Get(tw => tw.TeacherId == teacher.Id);
            if (techersWorkload == null) return null;

            IEnumerable<Group> groups = tea
            throw new Exception();
        }

        public IEnumerable<GroupBll> GetGroups(int facultyId, string userId, int subjectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubjectBll> GetSubjects(string userId)
        {
            TeacherBll teacher = GetTeacher(userId);
            if (teacher == null) throw new ArgumentNullException("userId", "Teacher with this Id does not exists");

            IEnumerable<TeachersWorkloadBll> teachersWorkloads = GetTeachersWorkloads(teacher.Id);
            IEnumerable<SubjectBll> subjects = teachersWorkloads.Select(tw => tw.Subject);
            return subjects;
        }

        
        public IEnumerable<SubjectBll> GetSubjects(int teacherId)
        {
            IEnumerable<TeachersWorkloadBll> teachersWorkloads = GetTeachersWorkloads(teacherId);
            IEnumerable<SubjectBll> subjects = teachersWorkloads.Select(tw => tw.Subject);
            return subjects;
        }

        public IEnumerable<int> GetTerm(int GroupId, int SubjectId, string userid)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(int teacherId)
        {
            IEnumerable<TeachersWorkload> teachersWorkloads= db.TeachersWorkloads.Get(tw => tw.TeacherId == teacherId);
            IEnumerable<TeachersWorkloadBll> teachersWorkloadBlls = mapper.Map<IEnumerable<TeachersWorkloadBll>>(teachersWorkloads);

            return teachersWorkloadBlls;

        }
        private TeacherBll GetTeacher(string userId)
        {
            if (userId == null) throw new ArgumentNullException("userId", "UserId is null or empty");
            Teacher teacher = db.Teachers.Get(t => t.IdentityId == userId).SingleOrDefault();
            TeacherBll teacherBll = mapper.Map<TeacherBll>(teacher);
            return teacherBll;
        }
    }
}
