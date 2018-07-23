using Microsoft.AspNetCore.Identity;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentReportBookDAL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        UserManager<IdentityUser> UserManager { get; }

        IPersonManager RersonManager { get; }

        //ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}
