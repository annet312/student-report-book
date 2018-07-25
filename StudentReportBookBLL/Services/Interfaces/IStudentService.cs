﻿using StudentReportBookBLL.Models;
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
        IEnumerable<FacultyBll> GetFaculties();
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
        IEnumerable<GroupBll> GetGroups(int facultyId);
        /// <summary>
        /// get groups of faculty where this teacher has workload
        /// </summary>
        /// <param name="faculty">needed faculty</param>
        /// <param name="teacherWorkloadId">teachers workload</param>
        /// <returns>groups</returns>
        //IEnumerable<GroupBll> GetGroups(int facultyId, int teachersWorkloadId);
        IEnumerable<StudentBll> GetStudents(int groupId);

    }
}