using AutoMapper;
using StudentReportBook.Models.Entities;
using StudentReportBookBLL.Identity.Model;
using StudentReportBookBLL.Models;

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

    public class FacultyToFacultyWithGroupsViewProfile : Profile
    {
        public FacultyToFacultyWithGroupsViewProfile()
        {
            CreateMap<Faculty, FacultyWithGroupsViewModel>()
                .ForMember(f => f.Id, map => map.MapFrom(fvm => fvm.Id))
                .ForMember(f => f.Name, map => map.MapFrom(fvm => fvm.Name))
                .ForMember(f => f.Groups, map => map.MapFrom(fvm => fvm.Groups));
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
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUserBll>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
    public class GradeBookToGradeBookViewProfile : Profile
    {
        public GradeBookToGradeBookViewProfile()
        {
            CreateMap<GradeBook, GradeBookViewModel>()
                .ForMember(gbvm => gbvm.Student, map => map.MapFrom(gb => gb.Student))
                .ForMember(gbvm => gbvm.Marks, map => map.MapFrom(gb => gb.Marks));
        }
    }

    public class MarkOfStudentProfile : Profile
    {
        public MarkOfStudentProfile()
        {
            CreateMap<MarkOfStudent, MarkOfStudentViewModel>()
                .ForMember(m => m.Student, map => map.MapFrom(mvm => mvm.Student))
                .ForMember(m => m.Marks, map => map.MapFrom(mvm => mvm.Marks));
        }
    }
    public class StudentToStudentViewProfile : Profile
    {
        public StudentToStudentViewProfile()
        {
            CreateMap<StudentBll, StudentViewModel>()
                .ForMember(svm => svm.Name, map => map.MapFrom(s => s.Name))
                .ForMember(svm => svm.Group, map => map.MapFrom(s => s.Group.Name))
                .ForMember(svm => svm.Faculty, map => map.MapFrom(s => s.Group.Faculty.Name))
                .ForMember(svm => svm.StudentCard, map => map.MapFrom(s => s.StudentCard))
                .ForMember(svm => svm.CurrentTerm, map => map.MapFrom(s => s.Group.CurrentTerm));
        }
    }
    public class MarkToMarkViewProfile : Profile
    {
        public MarkToMarkViewProfile()
        {
            CreateMap<MarkBll, MarkViewModel>()
                .ForMember(mvm => mvm.Grade, map => map.MapFrom(m => m.Grade))
                .ForMember(mvm => mvm.Subject, map => map.MapFrom(m => m.TeachersWorkload.Subject.Name))
                .ForMember(mvm => mvm.Teacher, map => map.MapFrom(m => m.TeachersWorkload.Teacher.Name))
                .ForMember(mvm => mvm.Term, map => map.MapFrom(m => m.TeachersWorkload.Term));
        }
    }
}
