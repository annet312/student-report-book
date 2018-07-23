using AutoMapper;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services.Interfaces;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentReportBookBLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork db;
        public TeacherService(IMapper mapper, IUnitOfWork db)
        {
            this.mapper = mapper;
            this.db = db;
        }
        public IEnumerable<TeacherBll> GetAllTeachers()
        {
            IEnumerable<Teacher> results;

            results = db.Teachers.GetAll();
            IEnumerable<TeacherBll> resultsBll = mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherBll>>(results);

            return resultsBll;
        }
    }
}
