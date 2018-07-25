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
        TeacherRepository teachers;
        StudentRepository students; 
        GroupRepository groups; 
        MarkRepository marks;
        TeachersWorkloadRepository teachersWorkloads; 
        SubjectRepository subjects; 
        IRepository<Faculty> faculties; 

        public UnitOfWork(AppDbContext context)
        {
            this.dbContext = context;
        }
        public StudentRepository Students
        {
            get
            {
                if (students == null)
                {
                    students = new StudentRepository(dbContext);
                }
                return students;
            }
        }

        public TeacherRepository Teachers
        {
            get
            {
                if (teachers == null)
                {
                    teachers = new TeacherRepository(dbContext);
                }
                return teachers;
            }
        }
        public SubjectRepository Subjects
        {
            get
            {
                if (subjects == null)
                {
                    subjects = new SubjectRepository(dbContext);
                }
                return subjects;
            }
        }
        public GroupRepository Groups
        {
            get
            {
                if (groups == null)
                {
                    groups = new GroupRepository(dbContext);
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
        public TeachersWorkloadRepository TeachersWorkloads
        {
            get
            {
                if (teachersWorkloads == null)
                {
                    teachersWorkloads = new TeachersWorkloadRepository(dbContext);
                }
                return teachersWorkloads;
            }
        }
        public MarkRepository Marks
        {
            get
            {
                if (marks == null)
                {
                    marks = new MarkRepository(dbContext);
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
