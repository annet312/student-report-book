using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;

namespace StudentReportBookDAL.Repositories
{
    public class PersonManager : IPersonManager
    {
        private readonly AppDbContext db;

        public PersonManager(AppDbContext context)
        {
            db = context;
        }

        public void Dispose()
        {
            db.Dispose();
        }
       
        public void Create(Person person)
        {
            db.People.Add(person);
            db.SaveChanges();
        }
    }
}
