﻿
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentReportBook.Data;
using StudentReportBook.Helpers;
using StudentReportBook.Models.Entities;
using StudentReportBook.ViewModel;

namespace StudentReportBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public AccountsController(UserManager<AppUser> userManager, IMapper mapper, ApplicationDbContext appDbContext)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return new OkObjectResult("work");
        }
        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = mapper.Map<AppUser>(model);

            var result = await userManager.CreateAsync(userIdentity, model.Password);

            
            if (!result.Succeeded)  return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            await appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
            await appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}
