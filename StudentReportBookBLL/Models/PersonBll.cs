using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Models
{
    public class AppUserBll : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public long? FacebookId { get; set; }

        public string PictureUrl { get; set; }
    }
}
