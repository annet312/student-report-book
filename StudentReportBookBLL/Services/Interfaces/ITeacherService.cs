using System.Collections.Generic;
using StudentReportBookBLL.Models;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<TeacherBll> GetAllTeachers();

        IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(int teacherId);

        IEnumerable<SubjectBll> GetAllSubjects();

        void ChangeTerm(int teacherWorkloadId, int term);

        void ChangeGroup(int teacherWorkloadId, int groupId);

        void ChangeSubject(int teacherWorkloadId, int subjectId);

        TeachersWorkloadBll AddWorkload(int teacherId, int subjectId, int groupId, int term);
    }
}
