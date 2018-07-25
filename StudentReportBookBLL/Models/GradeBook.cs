using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Models
{
    public class GradeBook
    {
        public StudentBll Student { get; set; }

        public IEnumerable<MarkBll> Marks { get; set; }
    }
}
