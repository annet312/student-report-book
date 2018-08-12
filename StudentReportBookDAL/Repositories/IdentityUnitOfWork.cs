using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Interfaces;

namespace StudentReportBookDAL.Repositories
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private AppDbContext db;

        public IdentityUnitOfWork(AppDbContext dbContext, UserManager<IdentityUser> userManager, IPersonManager personManager, RoleManager<IdentityRole> roleManager)
        {
            db = dbContext;
            UserManager = userManager;
            RoleManager = roleManager;
            RersonManager = personManager;
        }

        public UserManager<IdentityUser> UserManager { get; }

        public RoleManager<IdentityRole> RoleManager { get; }

        public IPersonManager RersonManager { get; }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    RersonManager.Dispose();
                }
                disposed = true;
            }
        }
    }
}
