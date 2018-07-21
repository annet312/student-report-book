using Microsoft.AspNetCore.Identity;
using StudentReportBookBLL.Identity.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentReportBookBLL.Identity.Interface
{
    public interface IUserService// : IDisposable
    {
        ///<summary>
        ///Create new user
        ///</summary>
        Task<IdentityResult> Create(AppUserBll userBll, string password);
        ///<summary>
        ///Authentication of user
        /// </summary>
        Task<ClaimsIdentity> Authenticate(AppUserBll userBll);
    }
}
