using System.Collections.Generic;
using StudentReportBookBLL.Models;

namespace StudentReportBookBLL.Services.Interfaces
{
    public interface IMarkService
    {
        /// <summary>
        /// Get all marks of student 
        /// </summary>
        /// <param name="student">student who need marks</param>
        /// <returns>list of marks</returns>
        IEnumerable<MarkBll> GetAllMarks(StudentBll student);

        /// <summary>
        /// Get all marks of subject for pointed student
        /// </summary>
        /// <param name="teacherId">teacher who set grades</param>
        /// <param name="subjectId">subject</param>
        /// <param name="groupId">group of student</param>
        /// <param name="studentId">student</param>
        /// <returns>list of marks</returns>
        IEnumerable<MarkBll> GetAllMarksOfSubject(int teacherId, int subjectId, int groupId, int studentId);

        /// <summary>
        /// edit or add mark for pointed student by pointed teacher
        /// </summary>
        /// <param name="student">student</param>
        /// <param name="grade">mark</param>
        /// <param name="teachersWorkload">workload of teacher</param>
        /// <returns></returns>
        bool EditMark(StudentBll student, int grade, TeachersWorkloadBll teachersWorkload);
    }
}
