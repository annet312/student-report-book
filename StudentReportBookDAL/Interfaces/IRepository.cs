using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookDAL.Interfaces
{

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        //T Get(int id);


        IEnumerable<T> Get(/*Func<T, Boolean> predicate*/System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
    
}
