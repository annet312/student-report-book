using Microsoft.EntityFrameworkCore;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
                                            .Include(st => st.Identity);

            return teachers;
        }

        public IEnumerable<Teacher> Get(Expression<Func<Teacher, bool>> predicate)
        {
            IEnumerable<Teacher> teachers = dbContext.Teachers
                               .Where(predicate)
                               .Include(st => st.Identity);

            return teachers;
        }

        public void Add(Teacher teacher)
        {
            dbContext.Set<Teacher>().Add(teacher);
        }

        public void Update(Teacher teacher)
        {
            dbContext.Entry(teacher).State = EntityState.Modified;
            dbContext.Set<Teacher>().Attach(teacher);
        }

        public void Delete(Teacher teacher)
        {
            Teacher existing = dbContext.Set<Teacher>().Find(teacher);
            if (existing != null) dbContext.Set<Teacher>().Remove(existing);
        }

    }
}