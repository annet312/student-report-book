using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using StudentReportBookBLL.Auth;
using StudentReportBookBLL.Helpers;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBookBLL.Identity.Model;
using StudentReportBookDAL.Entities;
using StudentReportBookDAL.Interfaces;
using StudentReportBookBLL.Identity.Auth;

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

        public async Task<string> AuthenticateAsync(string userName, string password)
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

        public async Task<IdentityResult> CreateAsync(AppUserBll userBll, string password)
        {
            IdentityUser user = mapper.Map<AppUserBll, AppUser>(userBll);
            IdentityResult result = null;
            IdentityResult res = null;

            List<Task<IdentityResult>> createRoles = new List<Task<IdentityResult>>();

            if (await db.RoleManager.FindByNameAsync("Teacher") == null)
            {
               createRoles.Add(db.RoleManager.CreateAsync(new IdentityRole("Teacher")));
            }
            if (await db.RoleManager.FindByNameAsync("Student") == null)
            {
                createRoles.Add(db.RoleManager.CreateAsync(new IdentityRole("Student")));
            }
            if (await db.RoleManager.FindByNameAsync("Moderator") == null)
            {
                createRoles.Add(db.RoleManager.CreateAsync(new IdentityRole("Moderator")));
            }

            await Task.WhenAll(createRoles);

            result = await db.UserManager.CreateAsync(user, password);

            if(result.Succeeded)
            {
                res = await db.UserManager.AddToRoleAsync(user, userBll.Role);
                Person person;
                if ((userBll.Role == "Teacher") || userBll.Role == "Moderator")
                {
                    person = new Teacher()
                    {
                        FirstName = userBll.FirstName,
                        LastName = userBll.LastName,
                        IdentityId = userBll.Id,
                        Department = userBll.Department     
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
