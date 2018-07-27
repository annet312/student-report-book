using System;

namespace StudentReportBookDAL.Entities
{
    public class Mark
    {
        public int Id { get; set; }

        public int Grade
        {
            get; set;
        }        
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public DateTime Date { get; set; }

        public int TeachersWorkloadId { get; set; }

        public TeachersWorkload TeachersWorkload { get; set; }
    }
}
