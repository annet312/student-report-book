using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Repositories;
using System;

namespace StudentReportBookDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TeacherRepository Teachers { get; }

        StudentRepository Students { get; }

        GroupRepository Groups { get; }

        MarkRepository Marks { get; }

        TeachersWorkloadRepository TeachersWorkloads { get; }

        SubjectRepository Subjects { get; }

        IRepository<Faculty> Faculties { get; }

        void Save();
    }
}
