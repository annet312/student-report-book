using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;


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

        public IEnumerable<StudentBll> GetStudentWithoutGroup()
        {
            IEnumerable<Student> students = db.Students.Get(st => st.Group == null).OrderBy(st => st.Name);
            IEnumerable<StudentBll> studentBlls = mapper.Map<IEnumerable<StudentBll>>(students);

            return studentBlls;
        }

        public IEnumerable<FacultyBll> GetAllFaculties()
        {
            IEnumerable<Faculty> faculties = db.Faculties.GetAll().OrderBy(f => f.Name);
            IEnumerable<FacultyBll> facultiesBll = mapper.Map<IEnumerable<FacultyBll>>(faculties);

            return facultiesBll;
        }

        public IEnumerable<GroupBll> GetAllGroups()
        {
            IEnumerable<StudentReportBookDAL.Entities.Group> groups = db.Groups.GetAll().OrderBy(g => g.Name);
            IEnumerable<GroupBll> groupBlls = mapper.Map<IEnumerable<GroupBll>>(groups);

            return groupBlls;
        }

        public void SetGroupForStudent(int studentId, int groupId, string studentCard)
        {
            if (studentId < 0)
                throw new ArgumentException("Student Id not valid", "studentId");
            if (groupId < 0)
                throw new ArgumentException("Group Id not valid", "groupId");
            string pattern = @"^[0-9]{5}$";
            if (!Regex.IsMatch(studentCard, pattern, RegexOptions.IgnoreCase))
                throw new ArgumentException("Student card is invalid", "studentCard");
            if (db.Students.Get(st => st.StudentCard == studentCard).Any())
                throw new InvalidOperationException("Student with this student card already exist");

            Student student = db.Students.Get(st => st.Id == studentId).SingleOrDefault();
            if (student == null)
                throw new ArgumentException("Student with this id not found", "studentId");
            StudentReportBookDAL.Entities.Group group = db.Groups.Get(g => g.Id == groupId).SingleOrDefault();
            student.Group = group ?? throw new ArgumentException("Group with this id not found", "groupId");
            student.StudentCard = studentCard;
            db.Students.Update(student);
            db.Save();
        }
    }
}
