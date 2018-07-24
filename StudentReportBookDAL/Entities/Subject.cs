using System.Collections.Generic;

namespace StudentReportBookDAL.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TeachersWorkloadId { get; set; }

        public List<TeachersWorkload> TeachersWorkloads { get; set; }

    }
}
