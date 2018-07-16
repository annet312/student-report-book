using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentReportBook.Data;
using StudentReportBook.Models.Entities;

namespace StudentReportBook.Controllers
{
    [Authorize(Policy = "Student")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ClaimsPrincipal caller;
        private readonly ApplicationDbContext appDbContext;

        public StudentController(UserManager<AppUser> userManager, ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.caller = httpContextAccessor.HttpContext.User;
            this.appDbContext = appDbContext;
        }

        //GET api/student/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            //retrive the user info
            //HttpContext.User
            var userId = caller.Claims.Single(c => c.Type == "id");
            var customer = await appDbContext.Customers.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
                    {
                        Message = "This is secure data!",
                        customer.Identity.FirstName,
                        customer.Identity.LastName,
                        customer.Identity.PictureUrl,
                        customer.Identity.FacebookId,
                        customer.Location,
                        customer.Locale,
                        customer.Gender
            });
        }
    }
}