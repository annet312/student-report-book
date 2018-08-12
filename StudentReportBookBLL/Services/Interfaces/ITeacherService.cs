using StudentReportBookBLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface ITeacherService
    {
        TeacherBll GetTeacher(string teacherUserId);

        IEnumerable<TeacherBll> GetAllTeachers();

        IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(int teacherId);

        IEnumerable<SubjectBll> GetAllSubjects();

        void ChangeTerm(int teacherWorkloadId, int term);

        void ChangeGroup(int teacherWorkloadId, int groupId);

        void ChangeSubject(int teacherWorkloadId, int subjectId);
    }
}
