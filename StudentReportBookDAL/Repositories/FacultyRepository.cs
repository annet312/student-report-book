using Microsoft.EntityFrameworkCore;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentReportBookDAL.Repositories
{
    public class FacultyRepository : IRepository<Faculty> 
    {
        private readonly AppDbContext dbContext;
        public FacultyRepository(AppDbContext db)
        {
            this.dbContext = db;
        }
        public void Add(Faculty faculty)
        {
            dbContext.Set<Faculty>().Add(faculty);
        }

        public void Delete(Faculty faculty)
        {
            Faculty existing = dbContext.Set<Faculty>().Find(faculty);
            if (existing != null) dbContext.Set<Faculty>().Remove(existing);
        }

        public IEnumerable<Faculty> GetAll()
        {
            return dbContext.Set<Faculty>().Include(f => f.Groups).AsEnumerable();
        }


        public IEnumerable<Faculty> Get(System.Linq.Expressions.Expression<Func<Faculty, bool>> predicate)
        {
            return dbContext.Set<Faculty>().Include(f => f.Groups).Where(predicate).AsEnumerable<Faculty>();
        }

        public void Update(Faculty faculty)
        {
            dbContext.Set<Faculty>().Update(faculty);
        }
    }
}