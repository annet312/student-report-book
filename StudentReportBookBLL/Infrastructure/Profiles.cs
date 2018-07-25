using AutoMapper;
using StudentReportBookBLL.Identity.Model;
using StudentReportBookBLL.Models;
using StudentReportBookDAL.Entities;

namespace StudentReportBookBLL.Profiles
{
    public class PersonProfile : Profile
    {
        //public PersonProfile()
        //{
        //    CreateMap<Person, PersonBll>().ReverseMap();//.ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src.Identity));
 
        //}
    }
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, AppUserBll>();
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

                //.IncludeBase<PersonBll, Person>().ReverseMap();
        }
    }
    public class TeachersWorkloadProfile : Profile
    {
        public TeachersWorkloadProfile()
        {
            CreateMap<TeachersWorkloadBll, TeachersWorkload>().ForMember(tw => tw.TeacherId, map => map.MapFrom(tws => tws.Teacher.Id)).ReverseMap();
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
            CreateMap<SubjectBll, Subject>().ReverseMap();
        }
    }
    public class MarkProfile : Profile
    {
        public MarkProfile()
        {
            CreateMap<MarkBll, Mark>().ReverseMap();
        }
    }
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupBll, Group>().ReverseMap();
        }
    }
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<FacultyBll, Faculty>().ReverseMap();
        }
    }
}
