using StudentReportBookBLL.Models;

namespace StudentReportBookBLL.Services.Interfaces
{
    /// <summary>
    /// Get grade book of current user - student 
    /// </summary>
    public interface IGradeBookService
    {
        /// <summary>
        /// Get marks and information about student 
        /// </summary>
        /// <param name="userId">Identity id</param>
        /// <returns>grade book</returns>
        GradeBook GetMyMarks();
    }
}
