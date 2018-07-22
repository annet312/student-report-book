
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StudentReportBook.Auth;
using StudentReportBook.Helpers;
using StudentReportBook.Models;
using StudentReportBook.Models.Entities;
using StudentReportBook.ViewModel;
using StudentReportBook.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace StudentReportBook.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly int currentUser;//ClaimsPrincipal caller;
        private readonly UserManager<AppUser> userManager;
        private readonly IJwtFactory jwtFactory;
        private readonly JWTIssuerOptions jwtOptions;
        //private readonly ApplicationDbContext appDbContext;
        
        //private readonly IHttpContextAccessor httpContextAccessor;
        
        private string currentUser;

        public AuthController(UserManager<AppUser> userManager, 
                    IJwtFactory jwtFactory, 
                    IOptions<JWTIssuerOptions> jwtOptions, 
                    IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions.Value;
            //this.httpContextAccessor = httpContextAccessor;
           // currentUser = httpContextAccessor.CurrentUser();
            
        }
        
        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);

            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }
            currentUser = identity.Name;
            var jwt = await Tokens.GenerateJwt(identity, jwtFactory, credentials.UserName, jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        //GET api/auth/getCurrentUser
        [HttpGet("getCurrentUser")]
        //public async Task<IActionResult> Get()
        //{
        //    //var email = User.FindFirst("sub")?.Value;
        //    //var userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
        //    //var customer = HttpContext.User.Identity.Name;
        //    // var stringId = httpContextAccessor?.HttpContext?.User?.FindFirst(JwtR)
        //    return  new OkObjectResult(currentUser);
        //}
        //private Task<AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}