using System.Collections.Generic;
using StudentReportBookBLL.Models;

namespace StudentReportBookBLL.Services.Interfaces
{
    /// <summary>
    /// Service for work with teacher information
    /// </summary>
    public interface ITeacherService
    {
        /// <summary>
        /// get list of all teachers
        /// </summary>
        /// <returns>all teachers</returns>
        IEnumerable<TeacherBll> GetAllTeachers();
        /// <summary>
        /// get list of teachers workloads for pointed teacher
        /// </summary>
        /// <param name="teacherId">id of teacher</param>
        /// <returns>list of workloads</returns>
        IEnumerable<TeachersWorkloadBll> GetTeachersWorkloads(int teacherId);
        /// <summary>
        /// get all subjects
        /// </summary>
        /// <returns>list of subjects</returns>
        IEnumerable<SubjectBll> GetAllSubjects();
        /// <summary>
        /// Change term in teacher workload
        /// </summary>
        /// <param name="teacherWorkloadId">id of workload</param>
        /// <param name="term">new term</param>
        void ChangeTerm(int teacherWorkloadId, int term);
        /// <summary>
        /// change group in teacher workload
        /// </summary>
        /// <param name="teacherWorkloadId">id of workload</param>
        /// <param name="groupId">id of new group</param>
        void ChangeGroup(int teacherWorkloadId, int groupId);
        /// <summary>
        /// change subject in teacher workload
        /// </summary>
        /// <param name="teacherWorkloadId">id of workload</param>
        /// <param name="subjectId">id of new subject</param>
        void ChangeSubject(int teacherWorkloadId, int subjectId);
        /// <summary>
        /// Add new workload
        /// </summary>
        /// <param name="teacherId">teacher id</param>
        /// <param name="subjectId">subject id</param>
        /// <param name="groupId">group id</param>
        /// <param name="term">term</param>
        /// <exception cref="System.ArgumentException">One or more arguments is invalid</exception>
        /// <exception cref="System.InvalidOperationException">This workload is already exist</exception>
        /// <returns>new teacherworkload</returns>
        TeachersWorkloadBll AddWorkload(int teacherId, int subjectId, int groupId, int term);
        /// <summary>
        /// delete teachernworkload
        /// </summary>
        /// <param name="teacherWorkloadId">id of workload</param>
        /// <exception cref="System.ArgumentException">id is not valid</exception>
        /// <exception cref="System.InvalidOperationException">Workload was not found</exception>
        void DeleteWorkload(int teacherWorkloadId);
    }
}
