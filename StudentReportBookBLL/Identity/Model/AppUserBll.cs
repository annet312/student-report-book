using Microsoft.AspNetCore.Identity;
using StudentReportBookBLL.Models;

namespace StudentReportBookBLL.Identity.Model
{
    public class AppUserBll : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public long? FacebookId { get; set; }

        public string PictureUrl { get; set; }

        public int? PersonId { get; set; }

        public string Department { get; set; }

        public PersonBll Person { get; set; }
    }
}
