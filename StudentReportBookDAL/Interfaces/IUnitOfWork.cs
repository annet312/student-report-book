using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Teacher> Teachers { get; }

        IRepository<Student> Students { get; }

        IRepository<Group> Groups { get; }

        IRepository<Mark> Marks { get; }

        IRepository<TeachersWorkload> TeachersWorkloads { get; }

        IRepository<Subject> Subjects { get; }

        IRepository<Faculty> Faculties { get; }

        void Save();
    }
}
