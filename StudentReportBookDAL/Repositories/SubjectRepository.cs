using Microsoft.EntityFrameworkCore;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StudentReportBookDAL.Repositories
{
    public class SubjectRepository : IRepository<Subject>
    {
        private readonly AppDbContext dbContext;

        public SubjectRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Subject> GetAll()
        {
            IEnumerable<Subject> subjects = dbContext.Subjects
                                            .Include(sub => sub.TeachersWorkloads);

            return subjects;
        }

        public IEnumerable<Subject> Get(Expression<Func<Subject, bool>> predicate)
        {
            IEnumerable<Subject> subjects = dbContext.Subjects
                               .Where(predicate)
                               .Include(sub => sub.TeachersWorkloads);

            return subjects;
        }

        public void Add(Subject subject)
        {
            dbContext.Set<Subject>().Add(subject);
        }

        public void Update(Subject subject)
        {
            dbContext.Entry(subject).State = EntityState.Modified;
            dbContext.Set<Subject>().Attach(subject);
        }

        public void Delete(Subject subject)
        {
            Subject existing = dbContext.Set<Subject>().Find(subject);
            if (existing != null) dbContext.Set<Subject>().Remove(existing);
        }

    }
}