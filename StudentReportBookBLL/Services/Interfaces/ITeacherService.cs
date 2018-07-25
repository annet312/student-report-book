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

        IEnumerable<SubjectBll> GetSubjects(string userId);
        IEnumerable<SubjectBll> GetSubjects(int teacherId);

        //IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(string userId);
        //IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(int teacherId);
        TeachersWorkloadBll GetTeachersWorkload(int teacherId, GroupBll group, int subjectId);
        
        IEnumerable<FacultyBll> GetFaculties(string userId, int subjectId);
        IEnumerable<GroupBll> GetGroups(int facultyId, string userId, int subjectId);
        //IEnumerable<int> GetTerm(int GroupId, int SubjectId, string userid);


    }
}
