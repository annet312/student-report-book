using System.Collections.Generic;

namespace StudentReportBookBLL.Models
{
    public class GradeBook
    {
        public StudentBll Student { get; set; }

        public IEnumerable<MarkBll> Marks { get; set; }
    }
}
