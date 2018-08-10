using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.ViewModel
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Faculty { get; set; }
        public string StudentCard { get; set; }
        public int CurrentTerm { get; set; }
    }
}
