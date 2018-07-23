using AutoMapper;
using StudentReportBook.Models.Entities;
using StudentReportBookBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentReportBook.ViewModel.Mapping
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<TeacherBll, Teacher>();
        }
    }
}
