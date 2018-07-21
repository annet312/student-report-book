﻿using AutoMapper;
using StudentReportBookBLL.Models;
using StudentReportBookDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
}