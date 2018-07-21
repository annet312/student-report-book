using Microsoft.AspNetCore.Identity;
using StudentReportBookDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentReportBookDAL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        UserManager<AppUser> UserManager { get; }

        IPersonManager RersonManager { get; }

        //ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}
