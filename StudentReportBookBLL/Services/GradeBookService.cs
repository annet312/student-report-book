using AutoMapper;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBookBLL.Services
{
    public class GradeBookService : IGradeBookService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork db;
        private readonly IMarkService markService;
       // private readonly ITeacherService teacherService;
        private readonly IUserService userService;

        public GradeBookService(IUnitOfWork db, IMapper mapper, IMarkService markService,  IUserService userService)
        {
            this.mapper = mapper;
            this.db = db;
            this.markService = markService;
            //this.teacherService = teacherService;
            this.userService = userService;
        }

        

        GradeBook IGradeBookService.GetMyMarks()
        {
            var userName = userService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("userName is not available", userName);

            Student student =  db.Students.Get(s => s.Identity.UserName == userName).SingleOrDefault();
            if (student == null) throw new ArgumentException("This student does not exists", userName);

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
