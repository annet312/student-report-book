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
        private readonly IUnitOfWork unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Add(T entity)
        {
            unitOfWork.dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            T existing = unitOfWork.dbContext.Set<T>().Find(entity);
            if (existing != null) unitOfWork.dbContext.Set<T>().Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return unitOfWork.dbContext.Set<T>().AsEnumerable<T>();
        }


        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return unitOfWork.dbContext.Set<T>().Where(predicate).AsEnumerable<T>();
        }

        public void Update(T entity)
        {
            unitOfWork.dbContext.Entry(entity).State = EntityState.Modified;
            unitOfWork.dbContext.Set<T>().Attach(entity);
        }
    }
}