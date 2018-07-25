using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.Models.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }

    public class Student : Person
    {
        public string StudentCard { get; set; }

        public Group Group { get; set; }
    }

    public class Teacher : Person
    {
        public string Department { get; set; }

    }
}
