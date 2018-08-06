
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.Helpers;
using StudentReportBook.Models.Entities;
using StudentReportBook.ViewModel;
using StudentReportBookBLL.Identity.Interface;
using StudentReportBookBLL.Identity.Model;

namespace StudentReportBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
       // private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public AccountsController( IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

      
        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUserBll userIdentity = mapper.Map<RegistrationViewModel, AppUserBll>(model);

            IdentityResult result = await userService.Create(userIdentity, model.Password);//Manager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            }

            return new OkObjectResult("Account created");
        }


    }
}
