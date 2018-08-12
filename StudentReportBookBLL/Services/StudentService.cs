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
            IEnumerable<Student> students = db.Students.Get(st => st.Group == null);
            IEnumerable<StudentBll> studentBlls = mapper.Map<IEnumerable<StudentBll>>(students);

            return studentBlls;
        }

        public IEnumerable<FacultyBll> GetAllFaculties()
        {
            IEnumerable<Faculty> faculties = db.Faculties.GetAll();
            IEnumerable<FacultyBll> facultiesBll = mapper.Map<IEnumerable<FacultyBll>>(faculties);

            return facultiesBll;
        }

        public IEnumerable<GroupBll> GetAllGroups()
        {
            IEnumerable<Group> groups = db.Groups.GetAll();
            IEnumerable<GroupBll> groupBlls = mapper.Map<IEnumerable<GroupBll>>(groups);

            return groupBlls;
        }

        public void SetGroupForStudent(int studentId, int groupId)
        {
            if (studentId < 0)
                throw new ArgumentException("Student Id not valid", "studentId");
            if (groupId < 0)
                throw new ArgumentException("Group Id not valid", "groupId");
            Student student = db.Students.Get(st => st.Id == studentId).SingleOrDefault();
            if (student == null)
                throw new ArgumentException("Student with this id not found", "studentId");
            Group group = db.Groups.Get(g => g.Id == groupId).SingleOrDefault();
            if (group == null)
                throw new ArgumentException("Group with this id not found", "groupId");
            student.Group = group;

            db.Students.Update(student);
            db.Save();
        }
    }
}
