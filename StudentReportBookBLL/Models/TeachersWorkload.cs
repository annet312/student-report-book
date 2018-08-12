using System.Collections.Generic;

namespace StudentReportBookBLL.Models
{
    public class TeachersWorkloadBll
    {
        public int Id { get; set; }

        public int Term {get; set; }

        public GroupBll Group { get; set; }

        public SubjectBll Subject { get; set; }

        public TeacherBll Teacher { get; set; }

        public List<MarkBll> Marks { get; set; }
    }
}
