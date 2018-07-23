using Microsoft.EntityFrameworkCore;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentReportBookDAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext dbContext;
        public Repository(AppDbContext db)
        {
            this.dbContext = db;
        }
        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            T existing = dbContext.Set<T>().Find(entity);
            if (existing != null) dbContext.Set<T>().Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().AsEnumerable<T>();
        }


        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(predicate).AsEnumerable<T>();
        }

        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.Set<T>().Attach(entity);
        }
    }
}