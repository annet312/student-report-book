using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;

namespace StudentReportBookBLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork db;

        public TeacherService(IMapper mapper, IUnitOfWork db)
        {
            this.mapper = mapper;
            this.db = db;
        }

        public IEnumerable<TeacherBll> GetAllTeachers()
        {
            IEnumerable<Teacher> results;
            results = db.Teachers.GetAll().Where(t => t.Identity.Role != "Moderator");
            IEnumerable<TeacherBll> resultsBll = mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherBll>>(results);

            return resultsBll;
        }

        public IEnumerable<SubjectBll> GetAllSubjects()
        {
            IEnumerable<Subject> subjects = db.Subjects.GetAll();
            IEnumerable<SubjectBll> subjectsBll = mapper.Map<IEnumerable<SubjectBll>>(subjects);

            return subjectsBll;
        }

        public IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(int teacherId)
        {
            if (teacherId < 1)
                throw new ArgumentException("TeacherId is invalid", "teacherId");

            IEnumerable<TeachersWorkload> teachersWorkloads = db.TeachersWorkloads.Get(tw => tw.TeacherId == teacherId);
            IEnumerable<TeachersWorkloadBll> teachersWorkloadBlls = mapper.Map<IEnumerable<TeachersWorkloadBll>>(teachersWorkloads);

            return teachersWorkloadBlls;
        }

        public void ChangeTerm(int teacherWorkloadId, int term)
        {
            if (teacherWorkloadId < 0)
                throw new ArgumentException("TeacherworkloadId is invalid", "teacherWorkloadId");
            if ((term < 1) || (term > 12))
                throw new ArgumentException("Term is invalid", "term");
            TeachersWorkload tw = db.TeachersWorkloads.Get(t => t.Id == teacherWorkloadId).SingleOrDefault();
            if (tw == null)
                throw new ArgumentException("TeacherWorkload was not found", "teacherWorkloadId");
            tw.Term = term;
            db.TeachersWorkloads.Update(tw);
            db.Save();

            return;
        }

        public void ChangeGroup(int teacherWorkloadId, int groupId)
        {
            if (teacherWorkloadId < 0)
                throw new ArgumentException("TeacherworkloadId is invalid", "teacherWorkloadId");
            if (groupId < 1)
                throw new ArgumentException("GroupId is invalid", "groupId");
            TeachersWorkload tw = db.TeachersWorkloads.Get(t => t.Id == teacherWorkloadId).SingleOrDefault();
            if (tw == null)
                throw new ArgumentException("TeacherWorkload was not found", "teacherWorkloadId");
            Group group = db.Groups.Get(t => t.Id == groupId).SingleOrDefault();
            if (group == null)
                throw new ArgumentException("Group was not found", "groupId");
            tw.Group = group;
            db.TeachersWorkloads.Update(tw);
            db.Save();

            return;
        }

        public void ChangeSubject(int teacherWorkloadId, int subjectId)
        {
            if (teacherWorkloadId < 0)
                throw new ArgumentException("TeacherworkloadId is invalid", "teacherWorkloadId");
            if (subjectId < 1)
                throw new ArgumentException("SubjectId is invalid", "subjectId");
            TeachersWorkload tw = db.TeachersWorkloads.Get(t => t.Id == teacherWorkloadId).SingleOrDefault();
            if (tw == null)
                throw new ArgumentException("TeacherWorkload was not found", "teacherWorkloadId");
            Subject subject = db.Subjects.Get(t => t.Id == subjectId).SingleOrDefault();
            if (subject == null)
                throw new ArgumentException("Subject was not found", "suibjectId");
            tw.Subject = subject;
            db.TeachersWorkloads.Update(tw);
            db.Save();

            return;
        }
    }
}
