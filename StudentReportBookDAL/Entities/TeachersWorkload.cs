using System.Collections.Generic;

namespace StudentReportBookDAL.Entities
{
    public class TeachersWorkload
    {
        public int Id { get; set; }

        public int Term { get; set;}

        public int GroupId { get; set; }

        public Group Group { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

         public List<Mark> Marks { get; set; }
    }
}
