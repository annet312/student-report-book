using System;
using StudentReportBookBLL.Models;
using System.Collections.Generic;

namespace StudentReportBookBLL.Services.Interfaces
{/// <summary>
/// service for work with students and get info about faculties and group
/// </summary>
    public interface IStudentService
    {/// <summary>
    /// get student without group for moderator
    /// </summary>
    /// <returns>list of groups</returns>
        IEnumerable<StudentBll> GetStudentWithoutGroup();
        /// <summary>
        /// get all faculties
        /// </summary>
        /// <returns>list of faculties</returns>
        IEnumerable<FacultyBll> GetAllFaculties();
        /// <summary>
        /// get all groups
        /// </summary>
        /// <returns>list of groups</returns>
        IEnumerable<GroupBll> GetAllGroups();
        /// <summary>
        /// set group for student and student card
        /// </summary>
        /// <param name="studentId">id of student</param>
        /// <param name="groupId">group of student</param>
        /// <param name="studentCard">student card</param>
        /// <exception cref="ArgumentException">One or more arguments is invalid</exception>  
        /// <exception cref="InvalidOperationException">Student with this student card is already exist</exception>
        void SetGroupForStudent(int studentId, int groupId, string studentCard);
    }
}
