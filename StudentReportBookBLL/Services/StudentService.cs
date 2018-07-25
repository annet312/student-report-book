using AutoMapper;
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
        public StudentService(IMapper mapper, IUnitOfWork db)
        {
            this.mapper = mapper;
            this.db = db;
        }
        public IEnumerable<FacultyBll> GetFaculties()
        {
            IEnumerable<Faculty> faculties = db.Faculties.GetAll();
            IEnumerable<FacultyBll> facultiesBll = mapper.Map<IEnumerable<FacultyBll>>(faculties);

            return facultiesBll;
        }

        public IEnumerable<FacultyBll> GetFaculties(int teacherWorkloadsId)
        {
            IEnumerable<GroupBll> groups = GetGroups(teacherWorkloadsId);

            IEnumerable<FacultyBll> faculties = groups.Select(g => g.Faculty);

            return faculties;

        }

        private IEnumerable<GroupBll> GetGroups(int teachersWorkloadId)
        {
            IEnumerable<TeachersWorkload> teachersWorkloads = db.TeachersWorkloads.Get(tw => tw.Id == teachersWorkloadId);

            if (!teachersWorkloads.Any()) return null;

            IEnumerable<Group> groups = teachersWorkloads.Select(tw => tw.Group);
            IEnumerable<GroupBll> groupsBll = mapper.Map<IEnumerable<GroupBll>>(groups);

            return groupsBll;
        }

         IEnumerable<GroupBll> IStudentService.GetGroups(int facultyId)
        {
            IEnumerable<Group> groups = db.Groups.Get(g => g.Faculty.Id == facultyId);
            IEnumerable<GroupBll> groupsBll = mapper.Map<IEnumerable<GroupBll>>(groups);
            return groupsBll;
        }

        public  IEnumerable<GroupBll> GetGroups(int facultyId, int teachersWorkloadId)
        {
            IEnumerable<GroupBll> groups = this.GetGroups(teachersWorkloadId).Where(g => g.Faculty.Id == facultyId);

            return groups;
        }

        public IEnumerable<StudentBll> GetStudents(int groupId)
        {
            IEnumerable<Student> students = db.Students.Get(s => s.Group.Id == groupId);
            IEnumerable<StudentBll> studentsBll = mapper.Map<IEnumerable<StudentBll>>(students);
            return studentsBll;
        }
    }
}
