using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.ViewModel
{
    public class FacultyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class GroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class FacultyWithGroupsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupViewModel> Groups { get; set; }
    }
}
