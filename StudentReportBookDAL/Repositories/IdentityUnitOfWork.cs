using Microsoft.AspNetCore.Identity;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentReportBookDAL.Repositories
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private AppDbContext db;

        public IdentityUnitOfWork(AppDbContext dbContext, UserManager<IdentityUser> userManager, IPersonManager personManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = dbContext;
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.RersonManager = personManager;
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
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                   // roleManager.Dispose();
                    RersonManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
