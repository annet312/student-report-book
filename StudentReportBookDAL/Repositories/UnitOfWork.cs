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
        private readonly AppDbContext dbContext;
        IRepository<Teacher> teachers;
        IRepository<Student> students; 
        IRepository<Group> groups; 
        IRepository<Mark> marks;
        IRepository<TeachersWorkload> teachersWorkloads; 
        IRepository<Subject> subjects; 
        IRepository<Faculty> faculties; 

        public UnitOfWork(AppDbContext context)
        {
            this.dbContext = context;
        }
        public IRepository<Student> Students
        {
            get
            {
                if (students == null)
                {
                    students = new Repository<Student>(dbContext);
                }
                return students;
            }
        }

        public IRepository<Teacher> Teachers
        {
            get
            {
                if (teachers == null)
                {
                    teachers = new Repository<Teacher>(dbContext);
                }
                return teachers;
            }
        }
        public IRepository<Subject> Subjects
        {
            get
            {
                if (subjects == null)
                {
                    subjects = new Repository<Subject>(dbContext);
                }
                return subjects;
            }
        }
        public IRepository<Group> Groups
        {
            get
            {
                if (groups == null)
                {
                    groups = new Repository<Group>(dbContext);
                }
                return groups;
            }
        }
        public IRepository<Faculty> Faculties
        {
            get
            {
                if (faculties == null)
                {
                    faculties = new Repository<Faculty>(dbContext);
                }
                return faculties;
            }
        }
        public IRepository<TeachersWorkload> TeachersWorkloads
        {
            get
            {
                if (teachersWorkloads == null)
                {
                    teachersWorkloads = new Repository<TeachersWorkload>(dbContext);
                }
                return teachersWorkloads;
            }
        }
        public IRepository<Mark> Marks
        {
            get
            {
                if (marks == null)
                {
                    marks = new Repository<Mark>(dbContext);
                }
                return marks;
            }
        }

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
