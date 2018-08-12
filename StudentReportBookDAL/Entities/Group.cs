using System.Collections.Generic;

namespace StudentReportBookDAL.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }

        public int FacultyID { get; set; }

        public Faculty Faculty { get; set; }

        public int CurrentTerm { get; set; }
    }

    public class Faculty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Group> Groups { get; set; }
    }
}
