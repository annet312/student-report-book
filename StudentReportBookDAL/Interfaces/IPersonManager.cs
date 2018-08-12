using System;
using StudentReportBookDAL.Entities;

namespace StudentReportBookDAL.Interfaces
{
    public interface IPersonManager : IDisposable
    {
        void Create(Person person);
    }
}
