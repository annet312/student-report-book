using StudentReportBookBLL.Models;
using StudentReportBookDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface IGradeBookService
    {
        IEnumerable<MarkBll> GetAllMarks(StudentBll student);
    }
}
