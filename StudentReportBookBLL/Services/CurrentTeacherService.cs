using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;

namespace StudentReportBookBLL.Services
{
    public class CurrentTeacherService : ICurrentTeacherService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork db;
        private readonly IUserService userService;
        private readonly IMarkService markService;

        public CurrentTeacherService(IMapper mapper, IUnitOfWork db, IUserService userService, IMarkService markService)
        {
            this.mapper = mapper;
            this.db = db;
            this.userService = userService;
            this.markService = markService;
        }

        private TeacherBll GetCurrentTeacherId()
        {
            var userName = userService.GetCurrentUserId();
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName is not available", userName);
            Teacher teacher = db.Teachers.Get(s => s.Identity.UserName == userName).SingleOrDefault();
            if (teacher == null)
                throw new ArgumentException("This teacher does not exists", userName);
            var teacherBll = mapper.Map<TeacherBll>(teacher);

            return teacherBll;
        }

        public IEnumerable<TeachersWorkloadBll> GetTWOfCurrentTeacher()
        {
            TeacherBll teacher = GetCurrentTeacherId();
            IEnumerable<TeachersWorkload> teacherWorkloads = db.TeachersWorkloads.Get(tw => tw.TeacherId == teacher.Id).ToList();
            IEnumerable<TeachersWorkloadBll> workloadBlls = mapper.Map<IEnumerable<TeachersWorkloadBll>>(teacherWorkloads);

            return workloadBlls;
        }

        public IEnumerable<TeachersWorkloadBll> GetTWOfCurrentTeacher(int groupId, int subjectId)
        {
            TeacherBll teacher = GetCurrentTeacherId();
            IEnumerable<TeachersWorkload> teacherWorkloads = db.TeachersWorkloads.Get(tw => 
                                                                (tw.TeacherId == teacher.Id)
                                                             && (tw.Group.Id == groupId)
                                                             && (tw.Subject.Id == subjectId)).ToList();
            IEnumerable<TeachersWorkloadBll> workloadBlls = mapper.Map<IEnumerable<TeachersWorkloadBll>>(teacherWorkloads);

            return workloadBlls;
        }

        public IEnumerable<SubjectBll> GetSubjectsForCurrentTeacher()
        {
            IEnumerable<TeachersWorkloadBll> tws = GetTWOfCurrentTeacher();
            if (!tws.Any())
                return null;
            //groupby for unic subjects
            IEnumerable<SubjectBll> subjects = tws.GroupBy(s => s.Subject.Id)
                                                    .Select(tw => tw.First())
                                                    .Select(tw => tw.Subject).ToList();
            return subjects;
        }

        public IEnumerable<FacultyBll> GetFacultiesForCurrentTeacher(int subjectId)
        {
            if (subjectId < 1)
                throw new ArgumentException("SubjectId is not valid", "subjectId");
            IEnumerable<TeachersWorkloadBll> tws = GetTWOfCurrentTeacher();
            if (!tws.Any())
                return null;
            IEnumerable<FacultyBll> faculties = tws.Where(tw => tw.Subject.Id == subjectId)
                                                    .GroupBy(s => s.Group.Faculty.Id)
                                                    .Select(t => t.First())
                                                    .Select(tw => tw.Group.Faculty).ToList();

            return faculties;
        }

        public IEnumerable<GroupBll> GetGroupsForCurrentTeacher(int facultyId, int subjectId)
        {
            if (facultyId < 1)
                throw new ArgumentException("FacultyId is not valid", "facultyId");
            if (subjectId < 1)
                throw new ArgumentException("SubjectId is not valid", "subjectId");
            IEnumerable<TeachersWorkloadBll> tws = GetTWOfCurrentTeacher();
            if (!tws.Any())
                return null;
            IEnumerable<GroupBll> groups = tws.Where(tw =>
                                                   (tw.Group.Faculty.Id == facultyId) 
                                                && (tw.Subject.Id == subjectId))
                                               .GroupBy(s => s.Group.Id)
                                               .Select(t => t.First())
                                               .Select(tw => tw.Group).ToList();

            return groups;
        }

        public IEnumerable<MarkOfStudent> GetStudentsWithMarks(int groupId, int subjectId)
        {
            if (groupId < 1)
                throw new ArgumentException("GroupId is not valid", "groupId");
            if (subjectId < 1)
                throw new ArgumentException("SubjectId is not valid", "subjectId");
            IEnumerable<TeachersWorkloadBll> tws = GetTWOfCurrentTeacher();
            if (!tws.Any())
                return null;
            IEnumerable<Student> students = db.Students.Get(s => s.Group.Id == groupId);
            IEnumerable<StudentBll> studentBlls = mapper.Map<IEnumerable<StudentBll>>(students);
            List<MarkOfStudent> marksOfStudents = new List<MarkOfStudent>();
            
            foreach (var student in studentBlls)
            {
                marksOfStudents.Add(new MarkOfStudent()
                {
                    Student = student,
                    Marks = markService.GetAllMarksOfSubject(tws.FirstOrDefault().Teacher.Id, subjectId, groupId, student.Id).ToArray()
                });
            }
        
            return marksOfStudents;
        }

        public int[] GetTermsForCurrentTeacher(int groupId, int subjectId)
        {
            if (groupId < 1)
                throw new ArgumentException("GroupId is not valid", "groupId");
            if (subjectId < 1)
                throw new ArgumentException("SubjectId is not valid", "subjectId");
            IEnumerable<TeachersWorkloadBll> tws = GetTWOfCurrentTeacher(groupId, subjectId);
            if (!tws.Any())
                return null;
            int[] terms = tws.Select(t => t.Term).Distinct().ToArray();
            Array.Sort(terms);

            return terms;
        }

        public MarkBll EditMarkByCurrentTeacher(int studentId, int subjectId, int term, int grade)
        {
            if ((term < 1) || (term > 12))
                throw new ArgumentException("Term is not valid", "term");
            if ((grade < 0) || (grade > 5))
                throw new ArgumentException("Grade is not valid", "grade");
            StudentBll student = mapper.Map<StudentBll>(db.Students.Get(st => st.Id == studentId).SingleOrDefault());
            if (student == null)
                throw new ArgumentException("Student not found", "studentId");
            TeachersWorkloadBll tws = GetTWOfCurrentTeacher().Where(tw => (tw.Subject.Id == subjectId) 
                                                                       && (tw.Group.Id == student.Group.Id)
                                                                       && (tw.Term == term))
                                                                       .SingleOrDefault();
            MarkBll mark = markService.EditMark(student, grade, tws);

            return mark;
        }
    }
}
