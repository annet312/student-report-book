using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.Models.Entities
{
    public class EditMarkViewModel
    {
        public int studentId { get; set; }

        public int subjectId { get; set; }

        public int term { get; set; }

        public int grade { get; set; }
    }
}
