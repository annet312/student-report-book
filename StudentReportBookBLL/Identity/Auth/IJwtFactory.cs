
using Microsoft.AspNetCore.Identity;
using StudentReportBookBLL.Identity.Model;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentReportBookBLL.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(IdentityUser user, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string username, string id);
    }
}
