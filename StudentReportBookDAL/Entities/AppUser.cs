using Microsoft.AspNetCore.Identity;

namespace StudentReportBookDAL.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long? FacebookId { get; set; }

        public string PictureUrl { get; set; }

        public string Role { get; set; } 

        public string PersonId { get; set; }
        public Person Person { get; set; }
    }
}