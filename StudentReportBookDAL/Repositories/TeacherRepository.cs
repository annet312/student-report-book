using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;


namespace StudentReportBookDAL.Repositories
{
    public class TeacherRepository
    {
        private readonly AppDbContext dbContext;

        public TeacherRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Teacher> GetAll()
        {
            IEnumerable<Teacher> teachers = dbContext.Teachers
                                            .Include(st => st.Identity).AsEnumerable();

            return teachers;
        }

        public IEnumerable<Teacher> Get(Expression<Func<Teacher, bool>> predicate)
        {
            IEnumerable<Teacher> teachers = dbContext.Teachers
                               .Where(predicate)
                               .Include(st => st.Identity).AsEnumerable();

            return teachers;
        }

        public void Add(Teacher teacher)
        {
            dbContext.Set<Teacher>().Add(teacher);
        }

        public void Update(Teacher teacher)
        {
            dbContext.Set<Teacher>().Update(teacher);
        }

        public void Delete(Teacher teacher)
        {
            Teacher existing = dbContext.Set<Teacher>().Find(teacher);
            if (existing != null) dbContext.Set<Teacher>().Remove(existing);
        }

    }
}