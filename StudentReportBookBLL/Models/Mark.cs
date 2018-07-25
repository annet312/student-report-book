
using System;

namespace StudentReportBookBLL.Models
{
    public class MarkBll
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public StudentBll Student { get; set; }

        public DateTime Date { get; set; }

        public TeachersWorkloadBll TeachersWorkload { get; set; }
    }
}
