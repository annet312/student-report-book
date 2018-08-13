using System.Collections.Generic;
using StudentReportBookBLL.Models;

namespace StudentReportBookBLL.Services.Interfaces
{
    /// <summary>
    /// Service for get information about students
    /// </summary>
    public interface ICurrentTeacherService
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
        /// Get groups of faculty
        /// </summary>
        /// <param name="faculty">needed faculty</param>
        /// <returns>groups</returns>
        IEnumerable<GroupBll> GetGroupsForCurrentTeacher(int facultyId, int subjectId);
        
        /// <summary>
        /// Get list of students of pointed group in pointed subject with their marks
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="subjectId">subjectId</param>
        /// <returns>list of marks of students</returns>
        IEnumerable<MarkOfStudent> GetStudentsWithMarks(int groupId, int subjectId);
        
        /// <summary>
        /// Get terms for current teacher
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="subjectId">subjectId</param>
        /// <returns>get array with terms</returns>
        int[] GetTermsForCurrentTeacher(int groupId, int subjectId);

        /// <summary>
        /// Edit or add mark by current teacher for pointed student
        /// </summary>
        /// <param name="studentId">student</param>
        /// <param name="subjectId">subject</param>
        /// <param name="term">term</param>
        /// <param name="grade">grade</param>
        /// <returns>result of operation</returns>
        MarkBll EditMarkByCurrentTeacher(int studentId, int subjectId, int term, int grade);
    }
}
