using StudentReportBookBLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface IMarkService
    {
        IEnumerable<MarkBll> GetAllMarks(StudentBll student);

        IEnumerable<MarkBll> GetAllMarksOfGroup(TeacherBll teacher, GroupBll group);

        IEnumerable<MarkBll> GetAllMarksOfGroup(SubjectBll subject, GroupBll group);

        IEnumerable<MarkBll> GetAllMarksOfGroup(TeacherBll teacher, SubjectBll subject, GroupBll group);

    }
}
