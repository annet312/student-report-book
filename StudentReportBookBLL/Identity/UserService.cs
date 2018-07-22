using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBookBLL.Identity.Model;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentReportBookBLL.Identity
{
    public class UserService : IUserService
    {
        private readonly IIdentityUnitOfWork db;
        private readonly IMapper mapper;

        public UserService(IIdentityUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public Task<ClaimsIdentity> Authenticate(AppUserBll userBll)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> Create(AppUserBll userBll, string password)
        {
            IdentityResult result = await db.UserManager.CreateAsync(mapper.Map<AppUserBll, AppUser>(userBll), password);
            if(result.Succeeded)
            {
                Person person;
                if (userBll.Role == "Teacher")
                {
                    person = new Teacher()
                    {
                        FirstName = userBll.FirstName,
                        LastName = userBll.LastName,
                        IdentityId = userBll.Id,
                    };
                }
                else
                {
                    person = new Student()
                    {
                        FirstName = userBll.FirstName,
                        LastName = userBll.LastName,
                        IdentityId = userBll.Id,
                    };
                }
                db.RersonManager.Create(person);
            }
            return result;
        }
    }
}
