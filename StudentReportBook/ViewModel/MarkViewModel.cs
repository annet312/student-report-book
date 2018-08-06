using StudentReportBook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.ViewModel
{
    public class MarkViewModel
    {
        int Id { get; set; }

        public string Subject { get; set; }

        public string Teacher { get; set; }

        public int Term { get; set; }

        public int Grade { get; set; }

    }
    public class MarkOfStudentViewModel
    {
        public StudentViewModel Student { get; set; }

        public MarkViewModel[] Marks { get; set; }
    }
}
