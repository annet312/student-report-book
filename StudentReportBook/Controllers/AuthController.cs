
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
using StudentReportBook.ViewModel;
using AutoMapper;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBook.Helpers;

namespace StudentReportBook.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly int currentUser;//ClaimsPrincipal caller;
        //private readonly UserManager<AppUser> userManager;
        //private readonly IJwtFactory jwtFactory;
        //private readonly JWTIssuerOptions jwtOptions;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        
        //private string currentUser;

        public AuthController(IMapper mapper, IUserService userService)//UserManager<AppUser> userManager, 
                    //IJwtFactory jwtFactory, 
                    //IOptions<JWTIssuerOptions> jwtOptions)//, 
                    //IHttpContextAccessor httpContextAccessor)
        {
            // this.userManager = userManager;
            //this.jwtFactory = jwtFactory;
            //this.jwtOptions = jwtOptions.Value;
            this.mapper = mapper;
            this.userService = userService;
            
        }
        
        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //  var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            var jwt = await userService.Authenticate(credentials.UserName, credentials.Password);
            if (jwt == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }
            
            return new OkObjectResult(jwt);
        }


        
    }
}