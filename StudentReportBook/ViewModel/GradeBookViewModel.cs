using StudentReportBook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.ViewModel
{
    public class GradeBookViewModel
    {
        public Student Student { get; set; }

        public IEnumerable<Mark> Marks { get; set; }
    }
}
