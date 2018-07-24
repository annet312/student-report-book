﻿using StudentReportBook.Models.Entities;
using AutoMapper;
using StudentReportBookBLL.Identity.Model;

namespace StudentReportBook.ViewModel.Mapping
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUserBll>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
