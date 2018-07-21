using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Linq;

namespace StudentReportBookDAL.Repositories
{
    public class PersonManager : IPersonManager
    {
        private readonly AppDbContext db;

       

        public PersonManager(AppDbContext context)
        {
            this.db = context;
        }
        public void Create(Student student, string groupName)
        {
            Group group = db.Groups.Where(g => g.Name == groupName).SingleOrDefault();
            if(group == null)
            {
                throw new ArgumentException("This group doesn't exists", "groupName");
            }
            Student NewStudent = new Student {Group = group,
                                               FirstName = student.FirstName,
                                                LastName = student.LastName,
                                                StudentCard = student.StudentCard,
                                                Identity = student.Identity
                                                };
            db.Students.Add(NewStudent);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
       
        public void Create(Teacher teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
        }

    }
}
