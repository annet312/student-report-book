using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Models
{
    public class GroupBll
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<StudentBll> Students { get; set; }

        public FacultyBll Faculty { get; set; }

        public int CurrentTerm{ get;set; }
    }
    public class FacultyBll
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<GroupBll> Groups { get; set; }
    }
}
