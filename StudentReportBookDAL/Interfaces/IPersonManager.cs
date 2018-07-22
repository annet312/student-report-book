using StudentReportBookDAL.Entities;
using System;

namespace StudentReportBookDAL.Interfaces
{
    public interface IPersonManager : IDisposable
    {
        //void Create(Student student);

        void Create(Person person);
    }
}
