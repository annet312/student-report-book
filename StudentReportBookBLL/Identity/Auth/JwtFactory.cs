using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using StudentReportBookBLL.Identity.Model;
using StudentReportBookBLL.Models;
using StudentReportBookDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace StudentReportBookBLL.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JWTIssuerOptions jwtOptions;
        private readonly IIdentityUnitOfWork db;
        public JwtFactory(IOptions<JWTIssuerOptions> jwtOptions, IIdentityUnitOfWork db)
        {
            this.jwtOptions = jwtOptions.Value;
            this.db = db;
            ThrowIfInvalidOptions(this.jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(IdentityUser user, ClaimsIdentity identity)
        {
            IdentityOptions options = new IdentityOptions();
            var claims = new List<Claim>
           {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),

                  new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                    new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName)
                // identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
              //   identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
             };
            var userClaims = await db.UserManager.GetClaimsAsync(user);
            var userRoles = await db.UserManager.GetRolesAsync(user);
            claims.AddRange(userClaims);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await db.RoleManager.FindByNameAsync(userRole);
                if(role != null)
                {
                    var roleClaims = await db.RoleManager.GetClaimsAsync(role);
                    foreach(Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                notBefore: jwtOptions.NotBefore,
                expires: jwtOptions.Expiration,
                signingCredentials: jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JWTIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JWTIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JWTIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JWTIssuerOptions.JtiGenerator));
            }
        }
    }
}
