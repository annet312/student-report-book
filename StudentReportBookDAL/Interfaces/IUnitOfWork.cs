using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
//irepository<>

        void Save();
    }
}
