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
        private readonly IMarkService markService;

        public StudentService(IMapper mapper, IUnitOfWork db, IUserService userService, IMarkService markService)
        {
            this.mapper = mapper;
            this.db = db;
            this.userService = userService;
            this.markService = markService;
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
            IEnumerable<TeachersWorkloadBll> tws = GetTWOfCurrentTeacher();
            if (!tws.Any())
                return null;
            IEnumerable<GroupBll> groups = tws.Where(tw => (tw.Group.Faculty.Id == facultyId) 
                                                             && (tw.Subject.Id == subjectId))
                                               .GroupBy(s => s.Group.Id)
                                                    .Select(t => t.First())
                                            .Select(tw => tw.Group).ToList();
            return groups;
        }

        public IEnumerable<MarkOfStudent> GetStudentsWithMarks(int groupId, int subjectId)
        {
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
    }
}
