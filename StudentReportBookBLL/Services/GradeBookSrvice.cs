using AutoMapper;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//namespace StudentReportBookBLL.Services
//{
//    public class GradeBookService : IGradeBookService
//    {
//        private readonly IMapper mapper;
//        private readonly IUnitOfWork db;
//        private readonly IMarkService marks;
//        public GradeBookService(IUnitOfWork db, IMapper mapper, IMarkService marks)
//        {
//            this.mapper = mapper;
//            this.db = db;
//            this.marks = marks;
//        }

//        IEnumerable<MarkBll> IGradeBookService.GetAllMarks(StudentBll student)
//        {
//            var studentMarks = marks.GetAllMarks(student);
//            return studentMarks;
//        }

//    }
//}
