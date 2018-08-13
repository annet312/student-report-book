using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.Models.Entities
{
    public class AddWorkloadViewModel
    {
        public int TeacherId { get; set; }

        public int SubjectId { get; set; }

        public int GroupId { get; set; }

        public int Term { get; set; }
    }
}
