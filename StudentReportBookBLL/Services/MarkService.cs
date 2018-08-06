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
    public class MarkService : IMarkService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;
        private readonly ITeacherService teacherService;
        public MarkService(IMapper mapper, IUnitOfWork db, ITeacherService teacherService)
        {
            this.mapper = mapper;
            this.db = db;
            this.teacherService = teacherService;
        }

        private void AddMark(int grade, Student student, TeachersWorkload teachersWorkload, Boolean flSingle)
        {
            Mark mark = new Mark()
            {
                Grade = grade,
                Student = student,
                TeachersWorkload = teachersWorkload,
                Date = DateTime.Now
            };
            db.Marks.Add(mark);
            if (flSingle == true)
            {
                db.Save();
            }
            return;

        }

        public void AddMark(int grade, int studentId, string teacherUserId, int subjectId)
        {
            if ((grade < 0) || (grade > 5))
                throw new ArgumentException("Grade is not valid", "grade");
            
            if (string.IsNullOrEmpty(teacherUserId))
                throw new ArgumentNullException("TeacherUserId is null or empty", "teacherUserId");

            TeacherBll teacher = teacherService.GetTeacher(teacherUserId);
            if (teacher == null) throw new ArgumentException("Teacher does not exist", "teacherUserId");

            Student student = db.Students.Get(s => s.Id == studentId).SingleOrDefault();
            if (student == null) throw new ArgumentException("Student does not exist", "studentId");

            Subject subject = db.Subjects.Get(sub => sub.Id == subjectId).SingleOrDefault();
            if (subject == null)
                throw new ArgumentException("Subject was not found", "subjectId");

            TeachersWorkloadBll teachersWorkload = teacherService.GetTeachersWorkload(teacher.Id, mapper.Map<GroupBll>(student.Group), subjectId);
            TeachersWorkload tw = mapper.Map<TeachersWorkload>(teachersWorkload);
            AddMark(grade, student, tw, true);
        }

        public void AddMark(Dictionary<int, int> studentGrades, string teacherUserId, int subjectId)
        {
            if (string.IsNullOrEmpty(teacherUserId))
                throw new ArgumentNullException("TeacherUserId is null or empty", "teacherUserId");

            TeacherBll teacher = teacherService.GetTeacher(teacherUserId);
            if (teacher == null) throw new ArgumentException("Teacher does not exist", "teacherUserId");
            
            Subject subject = db.Subjects.Get(sub => sub.Id == subjectId).SingleOrDefault();
            if (subject == null)
                throw new ArgumentException("Subject was not found", "subjectId");

            Student student = db.Students.Get(st => st.Id == studentGrades.First().Key).SingleOrDefault();
            TeachersWorkloadBll teachersWorkload = teacherService.GetTeachersWorkload(teacher.Id, mapper.Map<GroupBll>(student.Group), subjectId);
            TeachersWorkload tw = mapper.Map<TeachersWorkload>(teachersWorkload);

            foreach (KeyValuePair<int, int> studentGrade in studentGrades)
            {
                Student studentKey = db.Students.Get(st => st.Id == studentGrade.Key).SingleOrDefault();
                if (student == null)
                    throw new ArgumentException("Some of students was not found");
                if ((studentGrade.Value > 6) || (studentGrade.Value < 0))
                    throw new ArgumentException("Some of grades was not valid");
                AddMark(studentGrade.Value, studentKey, tw, false);
            }
            db.Save();
        }
        public bool EditMark(int grade, int studentId, int teacherUserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MarkBll> GetAllMarksOfGroup(int subjectId, int groupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MarkBll> GetAllMarksOfGroup(string teacherUserId, int subjectId, int groupId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<MarkBll> IMarkService.GetAllMarks(StudentBll student)
        {
            //How need to work(with teacherworkload):
            IEnumerable<Mark> marks = db.Marks.Get(m => m.StudentId == student.Id);
            if (marks == null)
                return null;
            IEnumerable<MarkBll> marksbll = mapper.Map<IEnumerable<MarkBll>>(marks);
            
            
            return marksbll;
        }

        IEnumerable<MarkBll> IMarkService.GetAllMarksOfGroup(string teacherUserId, int groupId)
        {
            TeacherBll teacher = teacherService.GetTeacher(teacherUserId);
            IEnumerable<Mark> marks = db.Marks.Get(m => (m.TeachersWorkload.TeacherId == teacher.Id ) && (m.TeachersWorkload.GroupId == groupId));

            IEnumerable<MarkBll> marksbll = mapper.Map<IEnumerable<MarkBll>>(marks);

            return marksbll;
        }

    }
}
