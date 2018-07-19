using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookDAL.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public string IdentityId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SubjectId { get; set; }

        public List<PersonSubject> PersonSubjects { get; set; }

        public AppUser Identity;

        public Person()
        {
            PersonSubjects = new List<PersonSubject>();
        }
        public int TeachersWorkloadId { get; set; }
        public List<TeachersWorkload> TeachersWorkloads { get; set; }
    }

    public class Student : Person
    {
        public string StudentCard { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }
    }

    public class Teacher : Person
    {
        public string Department { get; set; }
    }
}
