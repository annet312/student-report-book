using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentReportBookBLL.Identity.Model;


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
        Task<string> Authenticate(string userName, string password);

        /// <summary>
        /// Return current (string) userId
        /// </summary>
        /// <returns></returns>
        string GetCurrentUserId();

        /// <summary>
        /// reeturn string that identify current users role
        /// </summary>
        /// <returns>role</returns>
        Task<IList<string>> GetCurrentUserRoleAsync();
    }
}
