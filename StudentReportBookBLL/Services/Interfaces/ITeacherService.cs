using StudentReportBookBLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<TeacherBll> GetAllTeachers();
    }
}
