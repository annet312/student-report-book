using StudentReportBookDAL.Entities;
using System;

namespace StudentReportBookDAL.Interfaces
{
    public interface IPersonManager : IDisposable
    {
        void Create(Person person, string groupName);

        void Create(Person person, Position pos);
    }
}
