using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;

namespace StudentReportBookDAL.Repositories
{
    public class StudentRepository : IRepository <Student>
    {
        private readonly AppDbContext dbContext;

        public StudentRepository( AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Student> GetAll()
        {
            IEnumerable<Student> students = dbContext.Students
                  .Include(st => st.Group).ThenInclude(g => g.Faculty)
                  .Include(st => st.Identity);
            return students; 
        }

        public IEnumerable<Student> Get(Expression<Func<Student, bool>> predicate)
        {
           
            IEnumerable<Student> students = dbContext.Students
                              .Where(predicate)
                              .Select(x => x)
                              .Include(stu => stu.Group)
               .ThenInclude(g => g.Faculty).AsEnumerable();



            return students;
        }

        public void Add(Student student)
        {
            dbContext.Set<Student>().Add(student);
        }

        public void Update(Student student)
        {
            dbContext.Set<Student>().Update(student);
        }

        public void Delete(Student student)
        {
            Student existing = dbContext.Set<Student>().Find(student);
            if (existing != null) dbContext.Set<Student>().Remove(existing);
        }
    }
}
