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
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<FacultyBll, FacultyViewModel>()
               .ForMember(f => f.Id, map => map.MapFrom(m => m.Id))
                .ForMember(f => f.Name, map => map.MapFrom(m => m.Name));
        }
    }
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupBll, GroupViewModel>()
               .ForMember(f => f.Id, map => map.MapFrom(m => m.Id))
                .ForMember(f => f.Name, map => map.MapFrom(m => m.Name));
        }
    }
}
