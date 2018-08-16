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
    public class TeachersWorkloadRepository : IRepository<TeachersWorkload>
    {
        private readonly AppDbContext dbContext;

        public TeachersWorkloadRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TeachersWorkload> GetAll()
        {
            IEnumerable<TeachersWorkload> teachersWorkloads = dbContext.TeachersWorkloads
                                            .Include(tw => tw.Group)
                                                    .ThenInclude(g => g.Faculty)
                                            .Include(tw => tw.Subject).AsEnumerable();
            return teachersWorkloads;
        }

        public IEnumerable<TeachersWorkload> Get(Expression<Func<TeachersWorkload, bool>> predicate)
        {
            IEnumerable<TeachersWorkload> teachersWorkloads = dbContext.TeachersWorkloads
                               .Where(predicate)
                                 .Include(tw => tw.Group)
                                 .ThenInclude(g => g.Faculty)
                                .Include(tw => tw.Subject)
                                .AsEnumerable();


            return teachersWorkloads;
        }

        public void Add(TeachersWorkload teachersWorkload)
        {
            dbContext.Set<TeachersWorkload>().Add(teachersWorkload);
        }

        public void Update(TeachersWorkload teachersWorkload)
        {
            dbContext.Set<TeachersWorkload>().Update(teachersWorkload);
        }

        public void Delete(int teachersWorkloadId)
        {
            TeachersWorkload existing = dbContext.Set<TeachersWorkload>().Find(teachersWorkloadId);
            if (existing != null) dbContext.Set<TeachersWorkload>().Remove(existing);
        }
    }
}