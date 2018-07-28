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
        private readonly IMarkService markService;
        private readonly ITeacherService teacherService;

        public GradeBookService(IUnitOfWork db, IMapper mapper, IMarkService markService, ITeacherService teacherService)
        {
            this.mapper = mapper;
            this.db = db;
            this.markService = markService;
            this.teacherService = teacherService;
        }

        GradeBook IGradeBookService.GetMyMarks(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException("userId is empty", userId);

            Student student =  db.Students.Get(s => s.IdentityId == userId).SingleOrDefault();
            if (student == null) throw new ArgumentException("This student does not exists", userId);

            var st = mapper.Map <StudentBll>(student);
            var gr = db.Groups.Get(gro => gro.Id == student.GroupId).SingleOrDefault();

            StudentBll studentBll = mapper.Map<StudentBll>(student);

            IEnumerable<MarkBll> studentMarks = markService.GetAllMarks(studentBll);

            var marks = new List<MarkBll>(studentMarks);
          
            GradeBook gradeBook = new GradeBook()
            {
                Student = studentBll,
                Marks = studentMarks
            };

            return gradeBook;
        }

    }
}
