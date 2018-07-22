﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookDAL.Entities
{
    public class Mark
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TeachersWorkloadId { get; set; }
        public TeachersWorkload TeachersWorkload { get; set; }
    }
}
