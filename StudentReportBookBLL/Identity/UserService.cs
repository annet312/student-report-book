using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using StudentReportBookBLL.Auth;
using StudentReportBookBLL.Helpers;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBookBLL.Identity.Model;
using StudentReportBookBLL.Models;
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
        private readonly IJwtFactory jwtFactory;
        private readonly JWTIssuerOptions jwtOptions;

        public UserService(IIdentityUnitOfWork uow, IMapper mapper, IJwtFactory jwtFactory, JWTIssuerOptions jwtOptions)
        {
            db = uow;
            this.mapper = mapper;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions;
        }

        public async Task<string> Authenticate(string userName, string password)
        {
            var identity = await GetClaimsIdentity(userName, password);
            if (identity == null)
            {
                return null;
            }
            var jwt = await Tokens.GenerateJwt(identity, jwtFactory, userName, jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return jwt;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await db.UserManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await db.UserManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            //Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public async Task<IdentityResult> Create(AppUserBll userBll, string password)
        {
            IdentityUser user = mapper.Map<AppUserBll, AppUser>(userBll);
            IdentityResult result = null;
            try
            {
                result = await db.UserManager.CreateAsync(user, password);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
