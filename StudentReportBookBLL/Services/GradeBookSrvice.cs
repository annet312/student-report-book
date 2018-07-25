using AutoMapper;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentReportBookBLL.Services
{
    public class GradeBookService : IGradeBookService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork db;
        private readonly IMarkService marks;
        public GradeBookService(IUnitOfWork db, IMapper mapper, IMarkService marks)
        {
            this.mapper = mapper;
            this.db = db;
            this.marks = marks;
        }

        GradeBook IGradeBookService.GetMyMarks(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException("userId is empty", userId);

            Student student = db.Students.Get(s => s.IdentityId == userId).SingleOrDefault();
            if (student == null) throw new ArgumentException("This student does not exists", userId);

            StudentBll studentBll = mapper.Map<StudentBll>(student);

            IEnumerable<MarkBll> studentMarks = marks.GetAllMarks(studentBll);

            GradeBook gradeBook = new GradeBook() { Student = studentBll,
                                                    Marks = studentMarks};

            return gradeBook;
        }

    }
}
