using StudentReportBook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.ViewModel
{
    public class TeacherWorkloadViewModel
    {
        public int Id { get; set; }

        public GroupViewModel Group { get; set; }

        public int Term { get; set; }

        public SubjectViewModel Subject { get; set; }
    }
}
