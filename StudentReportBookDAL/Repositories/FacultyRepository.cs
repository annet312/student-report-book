using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;

namespace StudentReportBookDAL.Repositories
{
    public class FacultyRepository : IRepository<Faculty> 
    {
        private readonly AppDbContext dbContext;

        public FacultyRepository(AppDbContext db)
        {
            dbContext = db;
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
            IEnumerable<Faculty> faculties = dbContext.Set<Faculty>()
                                            .Include(f => f.Groups).AsEnumerable();

            return faculties;
        }

        public IEnumerable<Faculty> Get(System.Linq.Expressions.Expression<Func<Faculty, bool>> predicate)
        {
            IEnumerable<Faculty> faculties = dbContext.Set<Faculty>()
                                            .Include(f => f.Groups)
                                            .Where(predicate).AsEnumerable();
            return faculties;
        }

        public void Update(Faculty faculty)
        {
            dbContext.Set<Faculty>().Update(faculty);
        }
    }
}