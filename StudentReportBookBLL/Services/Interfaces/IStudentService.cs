using StudentReportBookBLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Services.Interfaces
{
    /// <summary>
    /// Service for get information about students
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Get all faculties of University
        /// </summary>
        /// <returns>Faculties</returns>
        IEnumerable<SubjectBll> GetSubjectsForCurrentTeacher();

        /// <summary>
        /// Get subject for teacher who ask
        /// </summary>
        /// <returns>subjects </returns>
        IEnumerable<FacultyBll> GetFacultiesForCurrentTeacher(int subjectId);

        /// <summary>
        /// Get faculties where teacher with this Id has workload
        /// </summary>
        /// <param name="teachersWorkloadId">teachers workload</param>
        /// <returns>Faculties</returns>
       // IEnumerable<FacultyBll> GetFaculties(int teachersWorkloadId);
        /// <summary>
        /// Get groups of faculty
        /// </summary>
        /// <param name="faculty">needed faculty</param>
        /// <returns>groups</returns>
        IEnumerable<GroupBll> GetGroupsForCurrentTeacher(int facultyId, int subjectId);

        /// <summary>
        /// get groups of faculty where this teacher has workload
        /// </summary>
        /// <param name="faculty">needed faculty</param>
        /// <param name="teacherWorkloadId">teachers workload</param>
        /// <returns>groups</returns>
        //IEnumerable<GroupBll> GetGroups(int facultyId, int teachersWorkloadId);
       // IEnumerable<StudentBll> GetStudents(int groupId);
        IEnumerable<MarkOfStudent> GetStudentsWithMarks(int groupId, int subjectId);

        int[] GetTermsForCurrentTeacher(int groupId, int subjectId);

        /// <summary>
        /// Edit or add mark by current teacher for pointed student
        /// </summary>
        /// <param name="studentId">student</param>
        /// <param name="subjectId">subject</param>
        /// <param name="term">term</param>
        /// <param name="grade">grade</param>
        /// <returns>result of operation</returns>
        bool EditMarkByCurrentTeacher(int studentId, int subjectId, int term, int grade);

        IEnumerable<StudentBll> GetStudentWithoutGroup();

        IEnumerable<FacultyBll> GetAllFaculties();

        IEnumerable<GroupBll> GetAllGroups();

        void SetGroupForStudent(int studentId, int groupId);
    }
}
