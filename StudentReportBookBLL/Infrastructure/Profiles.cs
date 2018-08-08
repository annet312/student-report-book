using AutoMapper;
using StudentReportBookBLL.Identity.Model;
using StudentReportBookBLL.Models;
using StudentReportBookDAL.Entities;

namespace StudentReportBookBLL.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonBll>().ReverseMap();
        }
    }
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, AppUserBll>().ReverseMap();
        }
    }
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<TeacherBll, Teacher>()
                .ForMember(t => t.Id, map => map.MapFrom(ts => ts.Id)).ReverseMap()
                .ForMember(t => t.Name, map => map.MapFrom(ts => ts.Name)).ReverseMap()
                .ForMember(t => t.FirstName, map => map.MapFrom(ts => ts.FirstName)).ReverseMap()
                .ForMember(t => t.LastName, map => map.MapFrom(ts => ts.LastName)).ReverseMap()
                .ForMember(t => t.Identity, map => map.MapFrom(ts => ts.Identity)).ReverseMap()
                .ForMember(t => t.Identity.Id, map => map.MapFrom(ts => ts.Identity.Id))
                .ForMember(t => t.Department, map => map.MapFrom(ts => ts.Department)).ReverseMap();
            
        }
    }
    public class TeachersWorkloadProfile : Profile
    {
        public TeachersWorkloadProfile()
        {
            CreateMap<TeachersWorkloadBll, TeachersWorkload>().ForMember(tw => tw.TeacherId, map => map.MapFrom(tws => tws.Teacher.Id));
        }
    }
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentBll, Teacher>();//.IncludeBase<PersonBll, Person>().ReverseMap();
        }
    }
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectBll, Subject>();
        }
    }
    public class MarkProfile : Profile
    {
        public MarkProfile()
        {
            CreateMap<Mark, MarkBll>()
                 .ForMember(m => m.Id, map => map.MapFrom(mbll => mbll.Id))
                 .ForMember(m => m.Date, map => map.MapFrom(mbll => mbll.Date))
                 .ForMember(m => m.Grade, map => map.MapFrom(mbll => mbll.Grade))
                 .ForMember(m => m.Student, map => map.MapFrom(mbll => mbll.Student))
                 .ForMember(m => m.TeachersWorkload, map => map.MapFrom(mbll => mbll.TeachersWorkload));

        }
    }
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupBll, Group>();
        }
    }
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<FacultyBll, Faculty>();
        }
    }
}
