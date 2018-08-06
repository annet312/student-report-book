using StudentReportBookBLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface IMarkService
    {
        IEnumerable<MarkBll> GetAllMarks(StudentBll student);

        IEnumerable<MarkBll> GetAllMarksOfGroup(string teacherUserid, int groupId);

        IEnumerable<MarkBll> GetAllMarksOfGroup(int subjectId, int groupId);

        IEnumerable<MarkBll> GetAllMarksOfSubject(int teacherId, int subjectId, int groupId, int studentId);

        void AddMark(int grade, int studentId, string teacherUserId, int subjectId);

        void AddMark(Dictionary<int, int> studentGrades, string teacherUserId, int subjectId);

        Boolean EditMark(int grade, int studentId, int teacherUserId);
    }
}
