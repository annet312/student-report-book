using StudentReportBookBLL.Identity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Models
{
    //public class PersonBll
    //{
    //    public int Id { get; set; }

    //    public string Name { get; private set; }

    //    public string FirstName { get; set; }

    //    public string LastName { get; set; }

    //    public AppUserBll Identity { get; set; }
    //}

    public class StudentBll /*: PersonBll*/
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AppUserBll Identity { get; set; }
        public string StudentCard { get; set; }

        public GroupBll Group { get; set; }
    }

    public class TeacherBll /*: PersonBll*/
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AppUserBll Identity { get; set; }
        public string Department { get; set; }

    }
}
