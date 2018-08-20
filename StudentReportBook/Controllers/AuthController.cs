using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.ViewModel;
using AutoMapper;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBook.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace StudentReportBook.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public AuthController(IMapper mapper, IUserService userService)
        {
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

            var jwt = await userService.AuthenticateAsync(credentials.UserName, credentials.Password);

            if (jwt == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }
            
            return new OkObjectResult(jwt);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        //GET api/auth/getCurrentRole
        [HttpGet("getCurrentRole")]
        public async Task<string> GetCurrentRole()
        {
            var roles = await userService.GetCurrentUserRoleAsync();
            string role = roles.FirstOrDefault();
            return role;
        }
    }
}