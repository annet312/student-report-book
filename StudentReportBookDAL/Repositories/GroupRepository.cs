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
                                        .Include(g => g.Students).AsEnumerable();

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
            dbContext.Set<Group>().Update(group);
        }

        public void Delete(int groupId)
        {
            Group existing = dbContext.Set<Group>().Find(groupId);
            if (existing != null) dbContext.Set<Group>().Remove(existing);
        }
    }
}