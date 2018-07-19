using System.Collections.Generic;

namespace StudentReportBookDAL.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PersonSubject> PersonSubjects { get; set; }

        public Subject()
        {
            PersonSubjects = new List<PersonSubject>();
        }

        public int TeachersWorkloadId { get; set; }

        public List<TeachersWorkload> TeachersWorkloads { get; set; }

    }
}
