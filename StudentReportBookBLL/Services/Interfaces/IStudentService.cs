using StudentReportBookBLL.Models;
using System.Collections.Generic;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentBll> GetStudentWithoutGroup();

        IEnumerable<FacultyBll> GetAllFaculties();

        IEnumerable<GroupBll> GetAllGroups();

        void SetGroupForStudent(int studentId, int groupId, string studentCard);
    }
}
