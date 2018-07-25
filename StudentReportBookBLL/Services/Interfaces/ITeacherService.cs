using StudentReportBookBLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<TeacherBll> GetAllTeachers();

        IEnumerable<SubjectBll> GetSubjects(string userId);
        IEnumerable<SubjectBll> GetSubjects(int teacherId);

        //IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(string userId);
        //IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(int teacherId);
        //IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(int teacherid, int groupId);

        IEnumerable<FacultyBll> GetFaculties(string userId, int subjectId);

        IEnumerable<GroupBll> GetGroups(int facultyId, string userId, int subjectId);
        IEnumerable<int> GetTerm(int GroupId, int SubjectId, string userid);


    }
}
