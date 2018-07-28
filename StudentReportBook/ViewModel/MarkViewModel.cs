using StudentReportBook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.ViewModel
{
    public class MarkViewModel
    {
        public string Subject { get; set; }

        public string Teacher { get; set; }

        public int Term { get; set; }

        public int Grade { get; set; }

    }
}
