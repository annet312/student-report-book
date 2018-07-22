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
            get
            {
                return CurrentTerm;
            }
            set
            {
                if (value < 1)
                {
                    CurrentTerm = 1;
                }
                else if (value > 12)
                {
                    CurrentTerm = 12;
                }
                else
                {
                    CurrentTerm = value;
                }
            }
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
