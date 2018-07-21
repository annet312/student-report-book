using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookDAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
       // private StudentRepository studentRepository;
        public AppDbContext dbContext { get; }
        public UnitOfWork(AppDbContext context)
        {
            this.dbContext = context;
        }
        //public IRepository<Student> Students
        //{
        //    get
        //    {
        //        if (studentRepository == null)
        //        {
        //            studentRepository = new StudentRepository(dbContext);
        //        }
        //        return studentRepository;
        //    }
        //}

//        private readonly AppDbContext dbContext;

        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
