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
    public class GroupRepository : IRepository<Group>
    {
        private readonly AppDbContext dbContext;

        public GroupRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Group> GetAll()
        {
            IEnumerable<Group> groups = dbContext.Groups
                                        .Include(g => g.Faculty)
                                        .Include(g => g.Students);

            return groups;
        }

        public IEnumerable<Group> Get(Expression<Func<Group, bool>> predicate)
        {
            IEnumerable<Group> groups = dbContext.Groups
                               .Where(predicate)
                               .Include(g => g.Students)
                               .Include(g => g.Faculty).AsEnumerable();

            return groups;
        }

        public void Add(Group group)
        {
            dbContext.Set<Group>().Add(group);
        }

        public void Update(Group group)
        {
            dbContext.Entry(group).State = EntityState.Modified;
            dbContext.Set<Group>().Attach(group);
        }

        public void Delete(Group group)
        {
            Group existing = dbContext.Set<Group>().Find(group);
            if (existing != null) dbContext.Set<Group>().Remove(existing);
        }
    }
}