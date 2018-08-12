using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace StudentReportBookDAL.Repositories
{
    public class ApplicationUserManager : UserManager<IdentityUser>
    {
        public ApplicationUserManager(IUserStore<IdentityUser> store,
                                       IOptions<IdentityOptions> identityOptions,
                                       IPasswordHasher<IdentityUser> passwordHasher,
                                       IEnumerable<IUserValidator<IdentityUser>> userValidators,
                                       IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
                                       ILookupNormalizer keyNormalizer,
                                       IdentityErrorDescriber errors,
                                       IServiceProvider services,
                                       ILogger<UserManager<IdentityUser>> logger)
            : base(store,
                 identityOptions,
                 passwordHasher,
                 userValidators,
                 passwordValidators,
                 keyNormalizer,
                 errors,
                 services,
                 logger)
        {

        }
    }
}