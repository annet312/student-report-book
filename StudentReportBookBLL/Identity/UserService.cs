using AutoMapper;
using Microsoft.AspNetCore.Http;
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
using System.Collections.Generic;

namespace StudentReportBookBLL.Identity
{
    public class UserService : IUserService
    {
        private readonly IIdentityUnitOfWork db;
        private readonly IMapper mapper;
        private readonly IJwtFactory jwtFactory;
        private readonly JWTIssuerOptions jwtOptions;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IIdentityUnitOfWork uow, IMapper mapper, IJwtFactory jwtFactory, JWTIssuerOptions jwtOptions, IHttpContextAccessor httpContextAccessor)
        {
            db = uow;
            this.mapper = mapper;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions;
            this.httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IList<string>> GetCurrentUserRoleAsync()
        {
            var userName = GetCurrentUserId();
            IdentityUser user = await db.UserManager.FindByNameAsync(userName);
            var role = db.UserManager.GetRolesAsync(user);
            return await role;
        }
        public  string GetCurrentUserId()
        {
            return db.UserManager.GetUserId(httpContextAccessor.HttpContext.User);
        }

        public async Task<string> Authenticate(string userName, string password)
        {
            IdentityUser user = await db.UserManager.FindByNameAsync(userName);
            var identity = await GetClaimsIdentity(user, password);

            if (identity == null)
            {
                return null;
            }
            var jwt = await Tokens.GenerateJwt(
                identity, 
                jwtFactory,
                user, 
                jwtOptions, 
                new JsonSerializerSettings { Formatting = Formatting.Indented }
                );

            return jwt;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(IdentityUser user, string password)
        {
            if ((user == null) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await db.UserManager.FindByNameAsync(user.UserName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await db.UserManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(user.UserName, userToVerify.Id));
            }

            //Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public async Task<IdentityResult> Create(AppUserBll userBll, string password)
        {
            IdentityUser user = mapper.Map<AppUserBll, AppUser>(userBll);
            IdentityResult result = null;
            IdentityResult res = null;

            if (await db.RoleManager.FindByNameAsync("Teacher") == null)
            {
                await db.RoleManager.CreateAsync(new IdentityRole("Teacher"));
            }
            if (await db.RoleManager.FindByNameAsync("Student") == null)
            {
                await db.RoleManager.CreateAsync(new IdentityRole("Student"));
            }
            if (await db.RoleManager.FindByNameAsync("Moderator") == null)
            {
                await db.RoleManager.CreateAsync(new IdentityRole("Moderator"));
            }

            try
            {
                result = await db.UserManager.CreateAsync(user, password);
            }
            catch(Exception e)
            {
                throw e;
            }

            if(result.Succeeded)
            {

                try
                {
                    res = await db.UserManager.AddToRoleAsync(user, userBll.Role);
                }
                catch(Exception e)
                {
                    throw e;
                }
                Person person;
                if ((userBll.Role == "Teacher") || userBll.Role == "Moderator")
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
