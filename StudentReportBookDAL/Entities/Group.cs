using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookDAL.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }

        public int FacultyID { get; set; }

        public Faculty Faculty { get; set; }
        public int CurrentTerm {
            get; set;
        }

        //public int TeacherWorkloadId { get; set; }
        //public List<TeachersWorkload> TeachersWorkloads { get; set; }
    }

    public class Faculty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Group> Groups { get; set; }
    }
}
