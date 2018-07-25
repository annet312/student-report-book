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
            CreateMap<Person, PersonBll>();
 
        }
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
            CreateMap<TeacherBll, Teacher>().IncludeBase<PersonBll, Person>();
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
            CreateMap<StudentBll, Student>().IncludeBase<PersonBll, Person>();
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
            CreateMap<MarkBll, Mark>();
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
