using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;

namespace StudentReportBookDAL.Repositories
{
    public class PersonManager : IPersonManager
    {
        private readonly IIdentityUnitOfWork db;

        public PersonManager(IIdentityUnitOfWork db)
        {
            this.db = db;
        }
        public void Create(Person person, string groupName)
        {
            
        }

        public void Dispose()
        {
            db.Dispose();
        }


        void IPersonManager.Create(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
