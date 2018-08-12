using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StudentReportBookDAL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        UserManager<IdentityUser> UserManager { get; }

        IPersonManager RersonManager { get; }

        RoleManager<IdentityRole> RoleManager { get; }

        Task SaveAsync();
    }
}
