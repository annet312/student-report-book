using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.Models.Entities
{
    public class Mark
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public Student Student { get; set; }

        public DateTime Date { get; set; }
    }
}
